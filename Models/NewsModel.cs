using FutbolowaJaskinia.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models
{
    public class NewsModel
    {
        [Key]
        public Guid Id { get; set; }

        [ValidUrlList]
        [Required]
        public List<string> PhotoUrl { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        public List<string> Likes { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateOfCreation { get; set; }
    }
}
