using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Favourite club")]
        public string FavouriteClub { get; set; }
    }
}
