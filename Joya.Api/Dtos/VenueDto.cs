namespace Joya.Api.Dtos
{
    public class VenueDto
    {
        public int Id { get; set; }
        public string VenueName { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public int TotalBooking { get; set; }
    }
}
