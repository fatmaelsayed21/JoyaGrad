using Domain.Models;
using Joya.Api.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Persentation;

namespace Joya.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }


        [HttpPost("BookNow")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!dto.VenueId.HasValue && !dto.DecorationId.HasValue && !dto.PhotographyAndVideographyId.HasValue && !dto.MusicEnvironmentId.HasValue)
            {
                return BadRequest("You must select at least one service to book.");
            }

            var buyerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(buyerId))
            {
                return Unauthorized("User ID not found in token.");
            }

            
            bool isAvailable = await _bookingService.IsServiceAvailable(dto.VenueId, dto.DecorationId, dto.PhotographyAndVideographyId, dto.MusicEnvironmentId, dto.EventDate);
            if (!isAvailable)
            {
                return BadRequest("One or more of the selected services are already booked for the chosen date.");
            }

            var booking = new Booking
            {
                EventDate = dto.EventDate,
                VenueId = dto.VenueId,
                DecorationId = dto.DecorationId,
                PhotographyAndVideographyId = dto.PhotographyAndVideographyId,
                MusicEnvironmentId = dto.MusicEnvironmentId,
                BuyerId = buyerId,
                Status = BookingStatus.Pending
            };

            try
            {
                var createdBooking = await _bookingService.CreateBookingAsync(booking);
                return CreatedAtAction(nameof(GetBookingById), new { id = createdBooking.BookingId }, createdBooking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }
    }
}



