using Domain.Contracts;
using Domain.Models;
using Joya.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joya.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotographyAndVideographyController : ControllerBase
    {
        private readonly IPhotographyAndVideogrpahy _photographyAndVideographyService;
        private readonly IConfiguration _configuration;

        public PhotographyAndVideographyController(IPhotographyAndVideogrpahy photographyAndVideographyService , IConfiguration configuration)
        {
            _photographyAndVideographyService = photographyAndVideographyService;
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(
                             [FromQuery] ServiceType? type,
                             [FromQuery] string? location,
                             [FromQuery] double? minPrice,
                             [FromQuery] double? maxPrice)
        {
            var items = await _photographyAndVideographyService.GetFilteredPhotographyAndVideographyAsync(type, location, minPrice, maxPrice);
            var baseUrl = _configuration["BaseUrl"];
            var result = items.Select(item => new PhotographyAndVideographyDto
            {
                Photography_VideographyID = item.PhotoGraphy_VideoGraphyID,
                ImageUrl = string.IsNullOrEmpty(item.ImageUrl) ? null : $"{baseUrl}/images/{item.ImageUrl}",
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

            var baseUrl = _configuration["BaseUrl"];
            var fullImageUrl = string.IsNullOrEmpty(item.ImageUrl) ? null : $"{baseUrl}/images/{item.ImageUrl}";

            var dto = new PhotographyAndVideographyDto
            {
                Photography_VideographyID = item.PhotoGraphy_VideoGraphyID,
                ImageUrl = fullImageUrl,
                Location = item.Location,
                Description = item.Description,
                Price = item.Price,
                CustomerReviews =item.CustomerReviews,
                Rating = item.Rating,
                TotalBookings = item.TotalBookings
            };

            return Ok(dto);
        }


        [Authorize(Roles = "Seller,Admin")]
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

            var baseUrl = _configuration["BaseUrl"];
            var fullImageUrl = string.IsNullOrEmpty(item.ImageUrl) ? null : $"{baseUrl}/images/{item.ImageUrl}";

            return CreatedAtAction(nameof(GetById), new { id = item.PhotoGraphy_VideoGraphyID }, new PhotographyAndVideographyDto
            {
                Photography_VideographyID = item.PhotoGraphy_VideoGraphyID,
                ImageUrl = fullImageUrl,
                Location = item.Location,
                Description = item.Description,
                Price = item.Price,
                Rating = item.Rating,
                TotalBookings = item.TotalBookings
            });
        }
        [Authorize(Roles = "Seller,Admin")]
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

            var baseUrl = _configuration["BaseUrl"];
            var fullImageUrl = string.IsNullOrEmpty(existingItem.ImageUrl) ? null : $"{baseUrl}/images/{existingItem.ImageUrl}";

            return Ok(new PhotographyAndVideographyDto
            {
                Photography_VideographyID = existingItem.PhotoGraphy_VideoGraphyID,
                ImageUrl = fullImageUrl,
                Location = existingItem.Location,
                Description = existingItem.Description,
                Price = existingItem.Price,
                Rating = existingItem.Rating,
                TotalBookings = existingItem.TotalBookings
            });
        }


        [Authorize(Roles = "Seller,Admin")]
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
