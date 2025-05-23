using Domain.Contracts;
using Domain.Models;
using Joya.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Joya.Api.Controllers
{
    [Route("api/photography")]
    public class PhotographyAndVideographyController : ControllerBase
    {
        private readonly IPhotographyAndVideogrpahy _photographyAndVideographyService;

        public PhotographyAndVideographyController(IPhotographyAndVideogrpahy photographyAndVideographyService)
        {
            _photographyAndVideographyService = photographyAndVideographyService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(
                             [FromQuery] ServiceType? type,
                             [FromQuery] string? location,
                             [FromQuery] double? minPrice,
                             [FromQuery] double? maxPrice)
        {
            var items = await _photographyAndVideographyService.GetFilteredPhotographyAndVideographyAsync(type, location, minPrice, maxPrice);

            var result = items.Select(item => new PhotographyAndVideographyDto
            {
                Photography_VideographyID = item.PhotoGraphy_VideoGraphyID,
                ImageUrl = item.ImageUrl,
                Location = item.Location,
                Description = item.Description,
                Price = item.Price,
                Rating = item.Rating,
                TotalBookings = item.TotalBookings
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _photographyAndVideographyService.GetPhotographyAndVideographyByIdAsync(id);
            if (item == null)
                return NotFound();

            var dto = new PhotographyAndVideographyDto
            {
                Photography_VideographyID = item.PhotoGraphy_VideoGraphyID,
                ImageUrl = item.ImageUrl,
                Location = item.Location,
                Description = item.Description,
                Price = item.Price,
                Rating = item.Rating,
                TotalBookings = item.TotalBookings
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePhotographyAndVideographyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = new PhotographyAndVideography
            {
                ImageUrl = dto.ImageUrl,
                Type = dto.Type,
                Location = dto.Location,
                Description = dto.Description,
                Price = dto.Price,
                Calender = dto.Calender,
                ProgramNumber = dto.ProgramNumber,
                SellerId = dto.SellerId
            };

            await _photographyAndVideographyService.AddPhotographyAndVideographyAsync(item);

            return CreatedAtAction(nameof(GetById), new { id = item.PhotoGraphy_VideoGraphyID }, new PhotographyAndVideographyDto
            {
                Photography_VideographyID = item.PhotoGraphy_VideoGraphyID,
                ImageUrl = item.ImageUrl,
                Location = item.Location,
                Description = item.Description,
                Price = item.Price,
                Rating = item.Rating,
                TotalBookings = item.TotalBookings
            });


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePhotographyAndVideographyDto dto)
        {
            if (id != dto.PhotoGraphy_VideoGraphyID)
                return BadRequest("Mismatched ID");

            var existingItem = await _photographyAndVideographyService.GetPhotographyAndVideographyByIdAsync(id);
            if (existingItem == null)
                return NotFound("Item not found");

            existingItem.ImageUrl = dto.ImageUrl;
            existingItem.Type = dto.Type;
            existingItem.Location = dto.Location;
            existingItem.Description = dto.Description;
            existingItem.Price = dto.Price;
            existingItem.Calender = dto.Calender;
            existingItem.ProgramNumber = dto.ProgramNumber;
            existingItem.SellerId = dto.SellerId;

            await _photographyAndVideographyService.UpdatePhotographyAndVideographyAsync(existingItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingItem = await _photographyAndVideographyService.GetPhotographyAndVideographyByIdAsync(id);
            if (existingItem == null)
                return NotFound("Item not found");

            await _photographyAndVideographyService.DeletePhotographyAndVideographyAsync(id);
            return NoContent();
        }
    }
}
