using System;
using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models
{
    public class ContactFormModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(150)]
        public string Content { get; set; }
    }
}
