using Lumia.DAL;
using Lumia.Models;
using Lumia.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lumia.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult>Index()    
        {
            List<Team> teams = await _context.Teams.ToListAsync();
            HomVM homvm = new HomVM()
            {
                Teams = teams
            };
            return View(homvm);
        }
    }
}