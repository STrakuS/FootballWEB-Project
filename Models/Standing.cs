using System;
using System.Collections.Generic;

namespace FootballWEB.Models;

public partial class Standing
{
    public int StandingId { get; set; }

    public int? TeamId { get; set; }

    public int? Played { get; set; }

    public int? Wins { get; set; }

    public int? Draws { get; set; }

    public int? Losses { get; set; }

    public int? GoalsFor { get; set; }

    public int? GoalsAgainst { get; set; }

    public int? Points { get; set; }

    public virtual Team? Team { get; set; }
}
