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
    public class PhotographyAndVideographyService : IPhotographyAndVideogrpahy
    {
        private readonly IGenericRepository<PhotographyAndVideography> _repository;

        public PhotographyAndVideographyService(IGenericRepository<PhotographyAndVideography> repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<PhotographyAndVideography>> GetAllPhotographyAndVideographyAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PhotographyAndVideography?> GetPhotographyAndVideographyByIdAsync(int id)
        {
            return await _repository.GetFirstOrDefaultAsync(
             filter: p => p.PhotoGraphy_VideoGraphyID == id,
             include: q => q.Include(p => p.Bookings)
         );
        }


        public async Task<IEnumerable<PhotographyAndVideography>> GetFilteredPhotographyAndVideographyAsync(
                            ServiceType? type,
                            string? location,
                            double? minPrice,
                            double? maxPrice)
        {
            var query = _repository.Query();

            if (type.HasValue)
                query = query.Where(p => p.Type == type.Value);

            if (!string.IsNullOrEmpty(location))
                query = query.Where(p => p.Location == location);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            return await query.ToListAsync();
        }



        public async Task AddPhotographyAndVideographyAsync(PhotographyAndVideography item)
        {
            await _repository.AddAsync(item);
            await _repository.SaveChangesAsync();
        }



        public async Task UpdatePhotographyAndVideographyAsync(PhotographyAndVideography item)
        {
            _repository.Update(item);
            await _repository.SaveChangesAsync();
        }

        public async Task DeletePhotographyAndVideographyAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
            }
        }

       

     

       
    }
}
