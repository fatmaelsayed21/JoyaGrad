using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IDecorationService
    {
        Task<IEnumerable<Decoration>> GetAllDecorationsAsync();
        Task<Decoration?> GetDecorationByIdAsync(int id);
        Task AddDecorationAsync(Decoration decoration);
        Task UpdateDecorationAsync(Decoration decoration);
        Task DeleteDecorationAsync(int id);

        Task<IEnumerable<Decoration>> GetFilteredDecorationsAsync(
            DecorationType? decorationType,
            string? location,
            double? minPrice,
            double? maxPrice,
            string? occasion
            
        );

    }
}
