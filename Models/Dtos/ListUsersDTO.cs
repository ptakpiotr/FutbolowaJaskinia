using System.Collections.Generic;

namespace FutbolowaJaskinia.Models.Dtos
{
    public class ListUsersDTO
    {
        public EditUserDTO User { get; set; }
        public List<string> UserRoles { get; set; } = new List<string>();
        public List<string> UserClaims { get; set; } = new List<string>();
    }
}
