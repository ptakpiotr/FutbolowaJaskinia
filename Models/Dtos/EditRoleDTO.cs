using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models.Dtos
{
    public class EditRoleDTO
    {
        public string RoleName { get; set; }
        [Display(Name = "Czy wybrana")]
        public bool IsChosen { get; set; }
    }
}
