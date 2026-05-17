using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballWEB.Models;

namespace FootballWEB.Controllers
{
    public class PlayersController : Controller
    {
        private readonly FootballDbContext _context;

        public PlayersController(FootballDbContext context)
        {
            _context = context;
        }

        // Sadece listeleme yapacak olan Index metodu
        public async Task<IActionResult> Index()
        {
            var players = await _context.Players.Include(p => p.Team).ToListAsync();
            return View(players);
        }
    }
}