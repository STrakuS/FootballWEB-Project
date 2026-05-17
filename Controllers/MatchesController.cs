using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballWEB.Models;

namespace FootballWEB.Controllers
{
    public class MatchesController : Controller
    {
        private readonly FootballDbContext _context;

        public MatchesController(FootballDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var matches = await _context.Matches
                .AsNoTracking()
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.Tournament)
                .OrderByDescending(m => m.MatchDate)
                .ToListAsync();

            return View(matches);
        }

        // GET: Matches/Create - Dropdown listelerinde isimleri gösterir
        public IActionResult Create()
        {
            // "TeamId" veritabanına gider, "TeamName" kullanıcıya görünür
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName");
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName");
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "TournamentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchId,MatchDate,HomeTeamId,AwayTeamId,HomeScore,AwayScore,TournamentId")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Hata durumunda listeleri tekrar doldur
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName", match.HomeTeamId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "TournamentName", match.TournamentId);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var match = await _context.Matches.FindAsync(id);
            if (match == null) return NotFound();

            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName", match.HomeTeamId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "TournamentName", match.TournamentId);
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatchId,MatchDate,HomeTeamId,AwayTeamId,HomeScore,AwayScore,TournamentId")] Match match)
        {
            if (id != match.MatchId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.MatchId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName", match.HomeTeamId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "TournamentName", match.TournamentId);
            return View(match);
        }

        // Diğer metodlar (Details/Delete) ihtiyaca göre kalabilir...
        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }
    }
}