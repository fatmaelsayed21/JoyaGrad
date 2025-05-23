using Domain.Contracts;
using Domain.Models;
using Joya.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Joya.Api.Controllers
{
    [Route("api/decoration")]
    public class DecorationController : ControllerBase
    {
        private readonly IDecorationService _decorationService;

        public DecorationController(IDecorationService decorationService)
        {
            _decorationService = decorationService;
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

            var result = items.Select(d => new DecorationDto
            {
                DecorationId = d.DecorationId,
                ImageUrl = d.ImageUrl,
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

            var dto = new DecorationDto
            {
                DecorationId = decoration.DecorationId,
                ImageUrl = decoration.ImageUrl,
                Location = decoration.Location,
                Description = decoration.Description,
                Price = decoration.Price,
                Rating = decoration.Rating,
                DecorationType = decoration.DecorationType,
                ProgramNumber = decoration.ProgramNumber,
                Calender = decoration.Calender,
                Occaison = decoration.Occaison,
                TotalBookings = decoration.TotalBookings
            };

            return Ok(dto);
        }

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
                Occaison = dto.Occasion,
                Rating = 0
            };

            await _decorationService.AddDecorationAsync(decoration);
            return CreatedAtAction(nameof(GetById), new { id = decoration.DecorationId }, decoration);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDecorationDto dto)
        {
            if (id != dto.DecorationId)
                return BadRequest("Mismatched ID");

            var existing = await _decorationService.GetDecorationByIdAsync(id);
            if (existing == null)
                return NotFound();

            var updated = new Decoration
            {
                DecorationId = dto.DecorationId,
                ImageUrl = dto.ImageUrl,
                DecorationType = dto.DecorationType,
                Location = dto.Location,
                Description = dto.Description,
                Price = dto.Price,
                Calender = dto.Calender,
                ProgramNumber = dto.ProgramNumber,
                SellerId = dto.SellerId,
                Occaison = dto.Occasion,
                Rating = existing.Rating // retain old rating
            };

            await _decorationService.UpdateDecorationAsync(updated);
            return NoContent();
        }

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
