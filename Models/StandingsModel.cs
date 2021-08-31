using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutbolowaJaskinia.Models
{

    public class StandingsModel
    {
        [Key]
        public Guid Id { get; set; }
        public Competition Competition { get; set; }
        public Season Season { get; set; }
        public virtual ICollection<Standing> Standings { get; set; }
    }

    public partial class Competition
    {
        public long Id { get; set; }
        public Area Area { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Plan { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
    }

    public partial class Area
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public partial class Season
    {
        public long Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public long CurrentMatchday { get; set; }
    }

    public partial class Standing
    {
        [Key]
        public Guid Id { get; set; }
        public string Stage { get; set; }
        public string Type { get; set; }
        public string Group { get; set; }
        public virtual ICollection<Table> Table { get; set; }
    }

    public partial class Table
    {
        [Key]
        public Guid Id { get; set; }
        public long Position { get; set; }
        public Team Team { get; set; }
        public long PlayedGames { get; set; }
        public string Form { get; set; }
        public long Won { get; set; }
        public long Draw { get; set; }
        public long Lost { get; set; }
        public long Points { get; set; }
        public long GoalsFor { get; set; }
        public long GoalsAgainst { get; set; }
        public long GoalDifference { get; set; }
    }

    public partial class Team
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Uri CrestUrl { get; set; }
    }
}
