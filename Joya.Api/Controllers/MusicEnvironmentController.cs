using Domain.Contracts;
using Domain.Models;
using Joya.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Persentation;

namespace Joya.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicEnvironmentController : ControllerBase
    {
        private readonly IMusicEnvironment _musicEnvironment;
        private readonly IConfiguration _configuration;

        public MusicEnvironmentController(IMusicEnvironment musicEnvironment , IConfiguration configuration)
        {
            _musicEnvironment = musicEnvironment;
           _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
        [FromQuery] MusicEnvironmentType? type,
        [FromQuery] string? location,
        [FromQuery] double? minPrice,
        [FromQuery] double? maxPrice,
        [FromQuery] string? occasion)
        {
            var result = await _musicEnvironment.GetFilteredMusicEnvironmentsAsync(type, location, minPrice, maxPrice, occasion);
            var baseUrl = _configuration["BaseUrl"];
            var mapped = result.Select(m => new MusicEnvironmentDto
            {
                MusicEnvironmentId = m.MusicEnvironmentId,
                ImageUrl = string.IsNullOrEmpty(m.ImageUrl) ? null : $"{baseUrl}/images/{m.ImageUrl}",
                Location = m.Location,
                Description = m.Description,
                Price = m.Price,
                Rating = m.Rating,
                MusicEnvironmentType = m.MusicEnvironmentType,
                ProgramNumber = m.ProgramNumber,
                Calendar = m.Calendar,
                TotalBookings = m.TotalBookings
            });

            return Ok(mapped);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _musicEnvironment.GetMusicEnvironmentByIdAsync(id);
            if (entity == null) return NotFound();

            var baseUrl = _configuration["BaseUrl"];


            var dto = new MusicEnvironmentDto
            {
                MusicEnvironmentId = entity.MusicEnvironmentId,
                ImageUrl = string.IsNullOrEmpty(entity.ImageUrl) ? null : $"{baseUrl}/images/{entity.ImageUrl}",
                Location = entity.Location,
                Description = entity.Description,
                Price = entity.Price,
                Rating = entity.Rating,
                MusicEnvironmentType = entity.MusicEnvironmentType,
                ProgramNumber = entity.ProgramNumber,
                Calendar = entity.Calendar,
                CustomerReviews = entity.CustomerReviews,
                TotalBookings = entity.TotalBookings
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMusicEnvironmentDto dto)
        {
            var entity = new MusicEnvironment
            {
                ImageUrl = dto.ImageUrl, 
                Location = dto.Location,
                Description = dto.Description,
                Price = dto.Price,
                Calendar = dto.Calendar,
                ProgramNumber = dto.ProgramNumber,
                SellerId = dto.SellerId,
                MusicEnvironmentType = dto.MusicEnvironmentType
            };

            await _musicEnvironment.AddMusicEnvironmentAsync(entity);

            var baseUrl = _configuration["BaseUrl"];
            var fullImageUrl = string.IsNullOrEmpty(entity.ImageUrl) ? null : $"{baseUrl}/images/{entity.ImageUrl}";

            return CreatedAtAction(nameof(GetById), new { id = entity.MusicEnvironmentId }, new MusicEnvironmentDto
            {
                MusicEnvironmentId = entity.MusicEnvironmentId,
                ImageUrl = fullImageUrl,
                Location = entity.Location,
                Description = entity.Description,
                Price = entity.Price,
                Rating = entity.Rating,
                TotalBookings = entity.TotalBookings
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMusicEnvironmentDto dto)
        {
            if (id != dto.MusicEnvironmentId)
                return BadRequest("Mismatched ID");

            var existing = await _musicEnvironment.GetMusicEnvironmentByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.ImageUrl = dto.ImageUrl;
            existing.Location = dto.Location;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Calendar = dto.Calendar;
            existing.ProgramNumber = dto.ProgramNumber;
            existing.SellerId = dto.SellerId;
            existing.MusicEnvironmentType = dto.MusicEnvironmentType;

            await _musicEnvironment.UpdateMusicEnvironmentAsync(existing);

            var baseUrl = _configuration["BaseUrl"];
            var fullImageUrl = string.IsNullOrEmpty(existing.ImageUrl) ? null : $"{baseUrl}/images/{existing.ImageUrl}";

            return Ok(new MusicEnvironmentDto
            {
                MusicEnvironmentId = existing.MusicEnvironmentId,
                ImageUrl = fullImageUrl,
                Location = existing.Location,
                Description = existing.Description,
                Price = existing.Price,
                Rating = existing.Rating,
                TotalBookings = existing.TotalBookings
            });
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _musicEnvironment.GetMusicEnvironmentByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _musicEnvironment.DeleteMusicEnvironmentAsync(id);
            return NoContent();
        }
    }

}

