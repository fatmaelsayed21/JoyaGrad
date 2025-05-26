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
    public class MusicEnvironmentService : IMusicEnvironment
    {
        private readonly IGenericRepository<MusicEnvironment> _musicEnvironmentRepository;

        public MusicEnvironmentService(IGenericRepository<MusicEnvironment> musicEnvironmentRepository)
        {
           _musicEnvironmentRepository = musicEnvironmentRepository;
        }

        public async Task<IEnumerable<MusicEnvironment>> GetAllMusicEnvironmentsAsync()
        {
            return await _musicEnvironmentRepository.GetAllAsync();
        }
        public  async Task<MusicEnvironment?> GetMusicEnvironmentByIdAsync(int id)
        {
            return await _musicEnvironmentRepository.GetFirstOrDefaultAsync(
           filter: m => m.MusicEnvironmentId == id,
           include: q => q
           .Include(m => m.Bookings)
           .Include(m=>m.CustomerReviews));
        }

        public async Task<IEnumerable<MusicEnvironment>> GetFilteredMusicEnvironmentsAsync(MusicEnvironmentType? type, string? location, double? minPrice, double? maxPrice, string? occasion)
        {
            {
                var query = _musicEnvironmentRepository.Query();

                if (type.HasValue)
                    query = query.Where(m => m.MusicEnvironmentType == type.Value);

                if (!string.IsNullOrEmpty(location))
                    query = query.Where(m => m.Location.Contains(location));

                if (minPrice.HasValue)
                    query = query.Where(m => m.Price >= minPrice.Value);

                if (maxPrice.HasValue)
                    query = query.Where(m => m.Price <= maxPrice.Value);

                if (!string.IsNullOrEmpty(occasion))
                    query = query.Where(m => m.Description.Contains(occasion)); 

                return await query.ToListAsync();
            }
        }

        public async Task AddMusicEnvironmentAsync(MusicEnvironment musicEnvironment)
        {
            await _musicEnvironmentRepository.AddAsync(musicEnvironment);
            await _musicEnvironmentRepository.SaveChangesAsync();
        }

        public async Task UpdateMusicEnvironmentAsync(MusicEnvironment musicEnvironment)
        {
            _musicEnvironmentRepository.Update(musicEnvironment);
            await _musicEnvironmentRepository.SaveChangesAsync();
        }

        public async Task DeleteMusicEnvironmentAsync(int id)
        {
            var item = await _musicEnvironmentRepository.GetByIdAsync(id);
            if (item != null)
            {
                _musicEnvironmentRepository.Delete(item);
                await _musicEnvironmentRepository.SaveChangesAsync();
            }
        }

       

       

      

      
    }
}
