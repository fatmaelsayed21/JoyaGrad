using Domain.Contracts;
using Domain.Models;
using Joya.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IConfiguration _configuration;

        public VenueController(IVenueService venueService ,IConfiguration configuration)
        {
            _venueService = venueService;
            _configuration = configuration;
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
            var baseUrl = _configuration["BaseUrl"];

            var result = venues.Select(v => new VenueDto
            {
                Id = v.VenueId,
                VenueName = v.VenueName,
                ImageUrl = string.IsNullOrEmpty(v.ImageUrl) ? null : $"{baseUrl}/images/{v.ImageUrl}",
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

            var baseUrl = _configuration["BaseUrl"];
            var fullImageUrl = string.IsNullOrEmpty(venue.ImageUrl) ? null : $"{baseUrl}/images/{venue.ImageUrl}";

            var dto = new VenueDto
            {
                Id = venue.VenueId,
                VenueName = venue.VenueName,
                ImageUrl = fullImageUrl,
                Location = venue.Location,
                Price = venue.Price,
                Rating = venue.Rating,
                TotalBooking = venue.TotalBookings,
                VenueType = venue.VenueType,
                Description = venue.Description,
                Calendar = venue.Calendar,
                Capacity = venue.Capacity,
                CustomerReviews = venue.CustomerReviews,
                SellerId = venue.SellerId
            };

            return Ok(dto);
        }


        [Authorize(Roles = "Seller,Admin")]
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
                ImageUrl = dto.ImageUrl,
                SellerId = dto.SellerId
            };

            await _venueService.AddVenueAsync(venue);

            var baseUrl = _configuration["BaseUrl"];
            var fullImageUrl = string.IsNullOrEmpty(venue.ImageUrl) ? null : $"{baseUrl}/images/{venue.ImageUrl}";

            return CreatedAtAction(nameof(GetById), new { id = venue.VenueId }, new VenueDto
            {
                Id = venue.VenueId,
                VenueName = venue.VenueName,
                ImageUrl = fullImageUrl,
                Location = venue.Location,
                Price = venue.Price,
                Rating = venue.Rating,
                TotalBooking = venue.TotalBookings
            });
        }


        [Authorize(Roles = "Seller,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVenueDto dto)
        {
            if (id != dto.VenueId)
                return BadRequest("Mismatched ID");

            var existingVenue = await _venueService.GetVenueByIdAsync(id);
            if (existingVenue == null)
                return NotFound("Venue not found");

            existingVenue.VenueName = dto.VenueName;
            existingVenue.VenueType = dto.VenueType;
            existingVenue.Description = dto.Description;
            existingVenue.Calendar = dto.Calendar;
            existingVenue.Price = dto.Price;
            existingVenue.Capacity = dto.Capacity;
            existingVenue.Location = dto.Location;
            existingVenue.ImageUrl = dto.ImageUrl;
            existingVenue.SellerId = dto.SellerId;

            await _venueService.UpdateVenueAsync(existingVenue);

            var baseUrl = _configuration["BaseUrl"];
            var fullImageUrl = string.IsNullOrEmpty(existingVenue.ImageUrl) ? null : $"{baseUrl}/images/{existingVenue.ImageUrl}";

            return Ok(new VenueDto
            {
                Id = existingVenue.VenueId,
                VenueName = existingVenue.VenueName,
                ImageUrl = fullImageUrl,
                Location = existingVenue.Location,
                Price = existingVenue.Price,
                Rating = existingVenue.Rating,
                TotalBooking = existingVenue.TotalBookings
            });
        }

        [Authorize(Roles = "Seller,Admin")]
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
