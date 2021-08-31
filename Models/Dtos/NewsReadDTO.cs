using System;
using System.Collections.Generic;

namespace FutbolowaJaskinia.Models.Dtos
{
    public class NewsReadDTO
    {
        public Guid Id { get; set; }

        public List<string> PhotoUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> Likes { get; set; }

        public DateTime DateOfCreation { get; set; }

        public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
    }
}
