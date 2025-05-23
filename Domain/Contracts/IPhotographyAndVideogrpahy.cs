using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IPhotographyAndVideogrpahy
    {
        Task<IEnumerable<PhotographyAndVideography>> GetAllPhotographyAndVideographyAsync();
        Task<PhotographyAndVideography?> GetPhotographyAndVideographyByIdAsync(int id);
        Task AddPhotographyAndVideographyAsync(PhotographyAndVideography item);
        Task UpdatePhotographyAndVideographyAsync(PhotographyAndVideography item);
        Task DeletePhotographyAndVideographyAsync(int id);

        Task<IEnumerable<PhotographyAndVideography>> GetFilteredPhotographyAndVideographyAsync(
            ServiceType? type,
            string? location,
            double? minPrice,
            double? maxPrice
        );
    }
}
