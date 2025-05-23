using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IVenueService
    {
        Task<IEnumerable<Venue>> GetAllVenuesAsync();
        Task<Venue?> GetVenueByIdAsync(int id);

        Task<IEnumerable<Venue>> GetFilteredVenuesAsync(VenueType? venueType, string? occasion, double? minPrice, double? maxPrice, int? capacity);

        Task AddVenueAsync(Venue venue);
        Task UpdateVenueAsync(Venue venue);
        Task DeleteVenueAsync(int id);
    }
}
