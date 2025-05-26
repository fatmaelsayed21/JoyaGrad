using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persentation
{
    public class BookingService
    {
        private readonly JoyaDbContext _context;

        public BookingService(JoyaDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            if (!booking.HasAtLeastOneService)
                throw new ArgumentException("Booking must have at least one service.");

           
            double totalPrice = 0;

            if (booking.VenueId.HasValue)
            {
                var venue = await _context.Venues.FindAsync(booking.VenueId.Value);
                if (venue == null) throw new Exception("Venue not found");
                totalPrice += venue.Price;
            }

            if (booking.DecorationId.HasValue)
            {
                var decoration = await _context.Decorations.FindAsync(booking.DecorationId.Value);
                if (decoration == null) throw new Exception("Decoration not found");
                totalPrice += decoration.Price;
            }

            if (booking.PhotographyAndVideographyId.HasValue)
            {
                var photoVid = await _context.PhotographyAndVideographies.FindAsync(booking.PhotographyAndVideographyId.Value);
                if (photoVid == null) throw new Exception("Photography & Videography service not found");
                totalPrice += photoVid.Price;
            }

            if (booking.MusicEnvironmentId.HasValue)
            {
                var musicEnv = await _context.MusicEnvironments.FindAsync(booking.MusicEnvironmentId.Value);
                if (musicEnv == null) throw new Exception("Music environment not found");
                totalPrice += musicEnv.Price;
            }

            booking.TotalPrice = totalPrice;
            booking.Status = BookingStatus.Pending;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Decoration)
                .Include(b => b.PhotographyAndVideography)
                .Include(b => b.MusicEnvironment)
                .Include(b => b.Buyer)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);


        }

        public async Task<bool> IsServiceAvailable(int? venueId, int? decorationId, int? photoVidId, int? musicEnvId, DateOnly eventDate)
        {
            var conflictingBooking = await _context.Bookings.AnyAsync(b =>
                b.EventDate == eventDate &&
                (
                    (venueId.HasValue && b.VenueId == venueId) ||
                    (decorationId.HasValue && b.DecorationId == decorationId) ||
                    (photoVidId.HasValue && b.PhotographyAndVideographyId == photoVidId) ||
                    (musicEnvId.HasValue && b.MusicEnvironmentId == musicEnvId)
                ) &&
                b.Status != BookingStatus.Cancelled 
            );

            return !conflictingBooking; 
        }

    }
}
