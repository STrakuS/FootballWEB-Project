using System;
using System.Collections.Generic;

namespace FootballWEB.Models;

public partial class Match
{
    public int MatchId { get; set; }

    public DateOnly? MatchDate { get; set; }

    public int? HomeTeamId { get; set; }

    public int? AwayTeamId { get; set; }

    public int? HomeScore { get; set; }

    public int? AwayScore { get; set; }

    public int? TournamentId { get; set; }

    public virtual Team ?HomeTeam { get; set; }

    public virtual Team ?AwayTeam { get; set; }

    public virtual ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new List<PlayerStatistic>();

    public virtual Tournament? Tournament { get; set; }
}
