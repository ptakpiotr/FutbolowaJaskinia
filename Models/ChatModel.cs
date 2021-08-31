using System;
using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models
{
    public class ChatModel
    {
        [Key]
        public Guid Id { get; set; }

        [EmailAddress]
        public string From { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
