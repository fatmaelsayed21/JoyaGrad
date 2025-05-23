using Domain.Contracts;
using Domain.Models;
using Joya.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Persentation;

namespace Joya.Api.Controllers
{
    [Route("api/music-environment")]
    public class MusicEnvironmentController : ControllerBase
    {
        private readonly IMusicEnvironment _musicEnvironment;

        public MusicEnvironmentController(IMusicEnvironment musicEnvironment)
        {
            _musicEnvironment = musicEnvironment;
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

            var mapped = result.Select(m => new MusicEnvironmentDto
            {
                MusicEnvironmentId = m.MusicEnvironmentId,
                ImageUrl = m.ImageUrl,
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

            var dto = new MusicEnvironmentDto
            {
                MusicEnvironmentId = entity.MusicEnvironmentId,
                ImageUrl = entity.ImageUrl,
                Location = entity.Location,
                Description = entity.Description,
                Price = entity.Price,
                Rating = entity.Rating,
                MusicEnvironmentType = entity.MusicEnvironmentType,
                ProgramNumber = entity.ProgramNumber,
                Calendar = entity.Calendar,
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
            return CreatedAtAction(nameof(GetById), new { id = entity.MusicEnvironmentId }, entity);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMusicEnvironmentDto dto)
        {
            if (id != dto.MusicEnvironmentId)
                return BadRequest("Mismatched ID");

            var existing = await _musicEnvironment.GetMusicEnvironmentByIdAsync(id);
            if (existing == null)
                return NotFound();

            var entity = new MusicEnvironment
            {
                MusicEnvironmentId = dto.MusicEnvironmentId,
                ImageUrl = dto.ImageUrl,
                Location = dto.Location,
                Description = dto.Description,
                Price = dto.Price,
                Calendar = dto.Calendar,
                ProgramNumber = dto.ProgramNumber,
                SellerId = dto.SellerId,
                MusicEnvironmentType = dto.MusicEnvironmentType
            };

            await _musicEnvironment.UpdateMusicEnvironmentAsync(entity);
            return NoContent();
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

