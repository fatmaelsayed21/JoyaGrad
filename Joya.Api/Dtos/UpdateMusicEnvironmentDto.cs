using System.ComponentModel.DataAnnotations;

namespace Joya.Api.Dtos
{
    public class UpdateMusicEnvironmentDto : CreateMusicEnvironmentDto
    {
        [Required]
        public int MusicEnvironmentId { get; set; }
    }
}
