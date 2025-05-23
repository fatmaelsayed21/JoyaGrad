using Domain.Models;

namespace Joya.Api.Dtos
{
    public class DecorationDto
    {
        public int DecorationId { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }

        public DecorationType? DecorationType { get; set; }
        public int ProgramNumber { get; set; }

        public DateOnly Calender { get; set; }

        public string Occaison { get; set; }

        public int TotalBookings { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
    }
}
