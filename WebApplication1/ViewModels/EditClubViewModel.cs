using WebApplication1.Data.Enum;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AdressId {  get; set; }
        public Adress Adress { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
