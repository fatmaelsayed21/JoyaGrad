using Joya.Api.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

namespace Joya.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public FavoritesController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpPost("toggle")]
        public IActionResult ToggleFavorite([FromBody] AddToFavoritesDto dto)
        {
            
            var buyerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(buyerId))
                return Unauthorized();

            string cacheKey = $"favorites_{buyerId}";

            
            if (!_cache.TryGetValue<List<AddToFavoritesDto>>(cacheKey, out var favorites))
            {
                favorites = new List<AddToFavoritesDto>();
            }

            
            var existing = favorites.FirstOrDefault(f =>
                f.VenueId == dto.VenueId &&
                f.DecorationId == dto.DecorationId &&
                f.PhotographyAndVideographyId == dto.PhotographyAndVideographyId &&
                f.MusicEnvironmentId == dto.MusicEnvironmentId);

            if (existing != null)
            {
               
                favorites.Remove(existing);
            }
            else
            {
               
                favorites.Add(dto);
            }

           
            _cache.Set(cacheKey, favorites, TimeSpan.FromMinutes(30));

          
            return Ok(favorites);
        }

        [HttpGet("All")]
        public IActionResult GetFavorites()
        {
            var buyerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(buyerId))
                return Unauthorized();

            string cacheKey = $"favorites_{buyerId}";

            if (!_cache.TryGetValue<List<AddToFavoritesDto>>(cacheKey, out var favorites))
            {
                favorites = new List<AddToFavoritesDto>();
            }

            return Ok(favorites);
        }
    }
}

