using System;
using System.Collections.Generic;

namespace FootballWEB.Models;

public partial class Tournament
{
    public int TournamentId { get; set; }

    public string TournamentName { get; set; } = null!;

    public string? City { get; set; }

    public string? Stadium { get; set; }

    public string? Season { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
