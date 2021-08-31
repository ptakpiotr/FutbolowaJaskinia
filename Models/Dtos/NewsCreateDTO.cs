using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models.Dtos
{
    public class NewsCreateDTO
    {
        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }
    }
}
