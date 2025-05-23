using Domain.Models;
using Joya.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Joya.Api.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly JoyaDbContext _context;

        public HomeController(JoyaDbContext context)
        {
            _context = context;
        }

        [HttpGet("homepage")]
            public async Task<IActionResult> GetHomePageData()
            {
                var venues = await _context.Venues
                    .Select(v => new VenueDto
                    {
                        Id = v.VenueId,
                        VenueName = v.VenueName,
                        ImageUrl = v.ImageUrl,
                        Location = v.Location,
                        Price = v.Price,
                        Rating = v.Rating
                    }).Take(5).ToListAsync();

                var decorations = await _context.Decorations
                    .Select(d => new DecorationDto
                    {
                        DecorationId = d.DecorationId,
                        ImageUrl = d.ImageUrl,
                        Location = d.Location,
                        Description = d.Description,
                        Price = d.Price,
                        Rating = d.Rating
                    }).Take(5).ToListAsync();

                var environments = await _context.MusicEnvironments
                    .Select(e => new MusicEnvironmentDto
                    {
                        MusicEnvironmentId = e.MusicEnvironmentId,
                        ImageUrl = e.ImageUrl,
                        Location = e.Location,
                        Description = e.Description,
                        Price = e.Price,
                        Rating = e.Rating
                    }).Take(5).ToListAsync();

                var photographyAndVideography = await _context.PhotographyAndVideographies
                    .Select(p => new PhotographyAndVideographyDto
                    {
                        Photography_VideographyID = p.PhotoGraphy_VideoGraphyID,
                        ImageUrl = p.ImageUrl,
                        Location = p.Location,
                        Description = p.Description,
                        Price = p.Price,
                        Rating = p.Rating
                    }).Take(5).ToListAsync();

                var homePageData = new
                {
                    Venues = venues,
                    Decorations = decorations,
                    Environments = environments,
                    Photography = photographyAndVideography
                };

                return Ok(homePageData);
            }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterByCategory([FromQuery] string category)
        {
            switch (category.ToLower())
            {
                case "venue":
                    var venues = await _context.Venues
                        .Select(v => new VenueDto
                        {
                            Id = v.VenueId,
                            VenueName = v.VenueName,
                            ImageUrl = v.ImageUrl,
                            Location = v.Location,
                            Price = v.Price,
                            Rating = v.Rating
                        }).ToListAsync();
                    return Ok(venues);

                case "photographyAndVideography":
                    var photographyAndVideography = await _context.PhotographyAndVideographies
                        .Select(p => new PhotographyAndVideographyDto
                        {
                            Photography_VideographyID = p.PhotoGraphy_VideoGraphyID,
                            ImageUrl = p.ImageUrl,
                            Location = p.Location,
                            Description = p.Description,
                            Price = p.Price,
                            Rating = p.Rating
                        }).ToListAsync();
                    return Ok(photographyAndVideography);

                case "decoration":
                    var decorations = await _context.Decorations
                        .Select(d => new DecorationDto
                        {
                            DecorationId = d.DecorationId,
                            ImageUrl = d.ImageUrl,
                            Location = d.Location,
                            Description = d.Description,
                            Price = d.Price,
                            Rating = d.Rating
                        }).ToListAsync();
                    return Ok(decorations);

                case "environment":
                    var environments = await _context.MusicEnvironments
                        .Select(e => new MusicEnvironmentDto
                        {
                            MusicEnvironmentId = e.MusicEnvironmentId,
                            ImageUrl = e.ImageUrl,
                            Location = e.Location,
                            Description = e.Description,
                            Price = e.Price,
                            Rating = e.Rating
                        }).ToListAsync();
                    return Ok(environments);

                default:
                    return BadRequest("Invalid category");
            }
        }

    }
}

