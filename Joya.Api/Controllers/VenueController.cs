using Domain.Contracts;
using Domain.Models;
using Joya.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Joya.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
                 [FromQuery] VenueType? venueType,
                 [FromQuery] string? occasion,
                 [FromQuery] double? minPrice,
                 [FromQuery] double? maxPrice,
                 [FromQuery] int? capacity)
                {
                   var venues = await _venueService.GetFilteredVenuesAsync(venueType, occasion, minPrice, maxPrice, capacity);

            
            var result = venues.Select(v => new VenueDto
            {
                Id = v.VenueId,
                VenueName = v.VenueName,
                ImageUrl = v.ImageUrl,
                Location = v.Location,
                Price = v.Price,
                Rating = v.Rating,
                TotalBooking = v.TotalBookings
            });

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var venue = await _venueService.GetVenueByIdAsync(id);
            if (venue == null) return NotFound();

            return Ok(new
            {
                venue.VenueId,
                venue.VenueName,
                venue.VenueType,
                venue.Description,
                venue.Calendar,
                venue.Price,
                venue.Capacity,
                venue.Location,
                venue.Rating,
                venue.ImageUrl,
                venue.SellerId,
                TotalBookings = venue.TotalBookings
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVenueDto dto)
        {
            var venue = new Venue
            {
                VenueName = dto.VenueName,
                VenueType = dto.VenueType,
                Description = dto.Description,
                Calendar = dto.Calendar,
                Price = dto.Price,
                Capacity = dto.Capacity,
                Location = dto.Location,
                Rating = dto.Rating,
                ImageUrl = dto.ImageUrl,
                SellerId = dto.SellerId
            };

            await _venueService.AddVenueAsync(venue);
            return CreatedAtAction(nameof(GetById), new { id = venue.VenueId }, venue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVenueDto dto)
        {
            

            if (id != dto.VenueId)
                return BadRequest("Mismatched ID");

            var existingVenue = await _venueService.GetVenueByIdAsync(id);
            if (existingVenue == null)
                return NotFound("Venue not found");

            var venue = new Venue
            {
                VenueId = dto.VenueId,
                VenueName = dto.VenueName,
                VenueType = dto.VenueType,
                Description = dto.Description,
                Calendar = dto.Calendar,
                Price = dto.Price,
                Capacity = dto.Capacity,
                Location = dto.Location,
                Rating = dto.Rating,
                ImageUrl = dto.ImageUrl,
                SellerId = dto.SellerId
            };

            await _venueService.UpdateVenueAsync(venue);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingVenue = await _venueService.GetVenueByIdAsync(id);
            if (existingVenue == null)
                return NotFound("Venue not found");
            await _venueService.DeleteVenueAsync(id);
            return NoContent();
        }

    }
}
