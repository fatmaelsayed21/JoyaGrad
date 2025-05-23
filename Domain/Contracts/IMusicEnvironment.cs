using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IMusicEnvironment
    {
        Task<IEnumerable<MusicEnvironment>> GetAllMusicEnvironmentsAsync();
        Task<MusicEnvironment?> GetMusicEnvironmentByIdAsync(int id);
        Task AddMusicEnvironmentAsync(MusicEnvironment musicEnvironment);
        Task UpdateMusicEnvironmentAsync(MusicEnvironment musicEnvironment);
        Task DeleteMusicEnvironmentAsync(int id);

        Task<IEnumerable<MusicEnvironment>> GetFilteredMusicEnvironmentsAsync(
            MusicEnvironmentType? type,
            string? location,
            double? minPrice,
            double? maxPrice,
            string? occasion 
        );
    }
}
