using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persentation
{
    public class VenueService : IVenueService
    {
        private readonly IGenericRepository<Venue> _venueRepository;

        public VenueService(IGenericRepository<Venue> venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public async Task<IEnumerable<Venue>> GetAllVenuesAsync()
        {
            return await _venueRepository.GetAllAsync();
        }

        public async Task<Venue?> GetVenueByIdAsync(int id)
        {
            return await _venueRepository.GetFirstOrDefaultAsync
            (
                 filter: v => v.VenueId == id,
                 include: q => q.
                 Include(v => v.Bookings)
                 .Include(v => v.CustomerReviews)
            );
        }

        public async Task<IEnumerable<Venue>> GetFilteredVenuesAsync(
                 VenueType? venueType,
                 string? occasion,
                 double? minPrice,
                 double? maxPrice,
                 int? capacity)
        {
            var query = _venueRepository.Query();

            if (venueType.HasValue)
                query = query.Where(v => v.VenueType == venueType.Value);

            if (!string.IsNullOrEmpty(occasion))
                query = query.Where(v => v.Occasion == occasion);

            if (minPrice.HasValue)
                query = query.Where(v => v.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(v => v.Price <= maxPrice.Value);

            if (capacity.HasValue)
                query = query.Where(v => v.Capacity >= capacity.Value);

            return await query.ToListAsync();
        }


        public async Task AddVenueAsync(Venue venue)
        {
            await _venueRepository.AddAsync(venue);
            await _venueRepository.SaveChangesAsync();
        }

        public async Task UpdateVenueAsync(Venue venue)
        {
            _venueRepository.Update(venue);
            await _venueRepository.SaveChangesAsync();
        }

        public async Task DeleteVenueAsync(int id)
        {
            var venue = await _venueRepository.GetByIdAsync(id);
            if (venue != null)
            {
                _venueRepository.Delete(venue);
                await _venueRepository.SaveChangesAsync();
            }
        }

      

      
    }
}
