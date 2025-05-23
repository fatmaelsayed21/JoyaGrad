using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persentation
{
    public class DecorationService : IDecorationService
    {
        private readonly IGenericRepository<Decoration> _decorationRepository;

        public DecorationService(IGenericRepository<Decoration> decorationRepository)
        {
            _decorationRepository = decorationRepository;
        }
        public async Task<IEnumerable<Decoration>> GetAllDecorationsAsync()
        {

            return await _decorationRepository.GetAllAsync();
        }



        public async  Task<Decoration?> GetDecorationByIdAsync(int id)
        {
            return await _decorationRepository.GetFirstOrDefaultAsync(
            filter: d => d.DecorationId == id,
            include: q => q.Include(d => d.Bookings)
        );
        }


        public async Task<IEnumerable<Decoration>> GetFilteredDecorationsAsync(DecorationType? decorationType, string? location , double? minPrice, double? maxPrice, string? occasion)
        {
            {
                var query = _decorationRepository.Query();

                if (decorationType.HasValue)
                    query = query.Where(d => d.DecorationType == decorationType.Value);

                if (!string.IsNullOrEmpty(location))
                    query = query.Where(d => d.Location == location);

                if (minPrice.HasValue)
                    query = query.Where(d => d.Price >= minPrice.Value);

                if (maxPrice.HasValue)
                    query = query.Where(d => d.Price <= maxPrice.Value);

                if (!string.IsNullOrEmpty(occasion))
                    query = query.Where(d => d.Occaison == occasion);

                return await query.ToListAsync();
            }
        }

        public async Task AddDecorationAsync(Decoration decoration)
        {
            await _decorationRepository.AddAsync(decoration);
            await _decorationRepository.SaveChangesAsync();
        }

        public async Task UpdateDecorationAsync(Decoration decoration)
        {
            _decorationRepository.Update(decoration);
            await _decorationRepository.SaveChangesAsync();
        }

        public async Task DeleteDecorationAsync(int id)
        {
            var decoration = await _decorationRepository.GetByIdAsync(id);
            if (decoration != null)
            {
                _decorationRepository.Delete(decoration);
                await _decorationRepository.SaveChangesAsync();
            }
        }

       
    }
}
