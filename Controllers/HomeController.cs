using Azure.Core;
using FootballWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly FootballDbContext _context;

    public HomeController(FootballDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // 1. Puan Durumunu Getir
        ViewBag.Standings = _context.Standings
            .Include(s => s.Team)
            .OrderByDescending(s => s.Points)
            .ToList();

        // 2. Son Maçları Getir
        ViewBag.Matches = _context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .OrderByDescending(m => m.MatchDate)
            .Take(5)
            .ToList();

        // 3. Takımları Getir
        ViewBag.Teams = _context.Teams.ToList();

        // 4. Gol Kralını Getir
        ViewBag.GolKrali = _context.PlayerStatistics
            .Include(ps => ps.Player)
            .GroupBy(ps => ps.Player)
            .Select(g => new
            {
                Player = g.Key,
                TotalGoals = g.Sum(x => x.Goals)
            })
            .OrderByDescending(x => x.TotalGoals)
            .FirstOrDefault();

        // 5. Asist Kralını Getir
        ViewBag.AsistKrali = _context.PlayerStatistics
            .Include(ps => ps.Player)
            .GroupBy(ps => ps.Player)
            .Select(g => new
            {
                Player = g.Key,
                TotalAssists = g.Sum(x => x.Assists)
            })
            .OrderByDescending(x => x.TotalAssists)
            .FirstOrDefault();

        return View();
    }

  [HttpPost]
public IActionResult UpdateMatchScore(int matchId, int homeScore, int awayScore)
{
    var match = _context.Matches.FirstOrDefault(m => m.MatchId == matchId);

    if (match != null)
    {
        match.HomeScore = homeScore;
        match.AwayScore = awayScore;

        _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("EXEC FootballDB.dbo.sp_UpdateStandings");
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }


    [HttpPost]
    public IActionResult AddTeam(string teamName)
    {
        if (!string.IsNullOrEmpty(teamName))
        {
            Team newTeam = new Team()
            {
                TeamName = teamName
            };

            _context.Teams.Add(newTeam);
            _context.SaveChanges();
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }
}

