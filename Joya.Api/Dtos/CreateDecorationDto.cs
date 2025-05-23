using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Joya.Api.Dtos
{
    public class CreateDecorationDto
    {
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public DecorationType? DecorationType { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        public DateOnly Calender { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ProgramNumber { get; set; }

        [Required]
        public string SellerId { get; set; }

        [Required]
        public string Occasion { get; set; } 
    }
}
