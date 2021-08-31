using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword))]
        public string ConfirmNew { get; set; }
    }
}
