using Domain.Contracts;
using Domain.Models;
using Joya.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joya.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DecorationController : ControllerBase
    {
        private readonly IDecorationService _decorationService;
        private readonly IConfiguration _configuration;

        public DecorationController(IDecorationService decorationService , IConfiguration configuration)
        {
            _decorationService = decorationService;
           _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
        [FromQuery] DecorationType? type,
        [FromQuery] string? location,
        [FromQuery] double? minPrice,
        [FromQuery] double? maxPrice,
        [FromQuery] string? occasion)
        {
            var items = await _decorationService.GetFilteredDecorationsAsync(type, location, minPrice, maxPrice, occasion);
            var baseUrl = _configuration["BaseUrl"];

            var result = items.Select(d => new DecorationDto
            {
                DecorationId = d.DecorationId,
                ImageUrl = string.IsNullOrEmpty(d.ImageUrl) ? null : $"{baseUrl}/images/{d.ImageUrl}",
                Location = d.Location,
                Description = d.Description,
                Price = d.Price,
                Rating = d.Rating,
                DecorationType = d.DecorationType,
                ProgramNumber = d.ProgramNumber,
                Calender = d.Calender,
                Occaison = d.Occaison,
                TotalBookings = d.TotalBookings
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var decoration = await _decorationService.GetDecorationByIdAsync(id);
            if (decoration == null)
                return NotFound();

            var baseUrl = _configuration["BaseUrl"];
            var imageUrl = string.IsNullOrEmpty(decoration.ImageUrl) ? null : $"{baseUrl}/images/{decoration.ImageUrl}";

            var dto = new DecorationDto
            {
                DecorationId = decoration.DecorationId,
                ImageUrl = imageUrl,
                Location = decoration.Location,
                Description = decoration.Description,
                Price = decoration.Price,
                Rating = decoration.Rating,
                DecorationType = decoration.DecorationType,
                ProgramNumber = decoration.ProgramNumber,
                Calender = decoration.Calender,
                Occaison = decoration.Occaison,
                CustomerReviews = decoration.CustomerReviews,
                TotalBookings = decoration.TotalBookings
            };

            return Ok(dto);
        }


        [Authorize(Roles = "Seller,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDecorationDto dto)
        {
            var decoration = new Decoration
            {
                ImageUrl = dto.ImageUrl,
                DecorationType = dto.DecorationType,
                Location = dto.Location,
                Description = dto.Description,
                Price = dto.Price,
                Calender = dto.Calender,
                ProgramNumber = dto.ProgramNumber,
                SellerId = dto.SellerId, 
                Occaison = dto.Occasion
            };

            await _decorationService.AddDecorationAsync(decoration);
            return CreatedAtAction(nameof(GetById), new { id = decoration.DecorationId }, decoration);
        }


        [Authorize(Roles = "Seller,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDecorationDto dto)
        {
            if (id != dto.DecorationId)
                return BadRequest("Mismatched ID");

            var existing = await _decorationService.GetDecorationByIdAsync(id);
            if (existing == null)
                return NotFound();

         
            existing.ImageUrl = dto.ImageUrl;
            existing.DecorationType = dto.DecorationType;
            existing.Location = dto.Location;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Calender = dto.Calender;
            existing.ProgramNumber = dto.ProgramNumber;
            existing.SellerId = dto.SellerId;
            existing.Occaison = dto.Occasion;

            await _decorationService.UpdateDecorationAsync(existing);
            return NoContent();
        }

        [Authorize(Roles = "Seller,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _decorationService.GetDecorationByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _decorationService.DeleteDecorationAsync(id);
            return NoContent();
        }


    }
}
