using System;
using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models
{
    public class HighlightsModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Competition { get; set; }
        public Uri MatchviewUrl { get; set; }
        public Uri CompetitionUrl { get; set; }
        public Uri Thumbnail { get; set; }
        public string Date { get; set; }
    }
}
