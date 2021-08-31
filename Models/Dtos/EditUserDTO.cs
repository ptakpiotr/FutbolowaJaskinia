using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models.Dtos
{
    public class EditUserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [Display(Name = "Favourite club")]
        public string FavouriteClub { get; set; }
    }
}
