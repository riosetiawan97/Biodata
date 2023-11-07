using Biodata.Models.Domain;

namespace Biodata.Models
{
    public class ProfilViewModel
    {
        public IQueryable<Profil> Profil { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }

    }
}
