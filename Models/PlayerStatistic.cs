using System;
using System.Collections.Generic;

namespace FootballWEB.Models;

public partial class PlayerStatistic
{
    public int StatId { get; set; }

    public int? PlayerId { get; set; }

    public int? MatchId { get; set; }

    public int? Goals { get; set; }

    public int? Assists { get; set; }

    public int? YellowCards { get; set; }

    public int? RedCards { get; set; }

    public int? MinutesPlayed { get; set; }

    public virtual Match? Match { get; set; }

    public virtual Player? Player { get; set; }
}
