using System;
using System.Collections.Generic;

namespace FootballWEB.Models;

public partial class Player
{
    public int PlayerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Position { get; set; }

    public int? JerseyNumber { get; set; }

    public int? Age { get; set; }

    public int? TeamId { get; set; }

    public virtual ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new List<PlayerStatistic>();

    public virtual Team? Team { get; set; }
}
