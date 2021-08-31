using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        public string Email { get; set; }

        [Compare(nameof(Email))]
        public string ConfirmEmail { get; set; }
    }
}
