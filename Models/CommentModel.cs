using System;
using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models
{
    public class CommentModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string PostId { get; set; }

        [Required]
        [EmailAddress]
        public string From { get; set; }

        public string Comment { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateOfCreation { get; set; }
    }
}
