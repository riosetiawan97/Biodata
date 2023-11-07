using System.ComponentModel.DataAnnotations;

namespace Biodata.Models.Domain
{
    public class Profil
    {
        public int Id { get; set; }
        [Required]
        public string? NIK { get; set; }
        [Required]
        public string? Nama { get; set; }
        [Required]
        public string? Alamat { get; set; }
        public string? StatusPerkawinan { get; set; }
    }
}
