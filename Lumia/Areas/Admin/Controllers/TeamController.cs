using Lumia.DAL;
using Lumia.Models;
using Lumia.Utilites.Extensitions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lumia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context,IWebHostEnvironment env)
        {   
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Team>teams=await _context.Teams.ToListAsync(); 

            return View(teams);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(Team team)
        {
            if(!ModelState.IsValid)return View(team);
            if(team.Photo is not null)
            {
                if (!team.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "file tipi uygun deyil ");
                    return View(team);
                }
                if (!team.Photo.ValidateSize(2*1024))
                {
                    ModelState.AddModelError("Photo", "file olcusu uygun deyil ");
                    return View(team);
                }
            }
            string FileName = await team.Photo.CreateAsync(_env.WebRootPath, "assets", "img", "team");
            Team teams = new Team
            {
                Image=FileName,
                Name=team.Name,
                Description=team.Description,
                Position=team.Position,

            };
            await _context.Teams.AddAsync(teams);   
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Update(int id ) 
        {
            if (id <= 0) return BadRequest();
            Team exsist= await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (exsist==null) return BadRequest();
            return View(exsist);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,Team team)
        {
            if (id <= 0) return BadRequest();
            Team exsist = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if(!ModelState.IsValid) return View(team);
            if (team.Photo is not null)
            {
                if (!team.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "file tipi uygun deyil ");
                    return View(team);
                }
                if (!team.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "file olcusu uygun deyil ");
                    return View(team);
                }
                string FileName = await team.Photo.CreateAsync(_env.WebRootPath, "assets", "img", "team");
                exsist.Image.DelteAsyncc(_env.WebRootPath, "assets", "img", "team");
                exsist.Image = FileName;
            }
             exsist.Name=team.Name;
            exsist.Description=team.Description;
            exsist.Position=team.Position;  
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Delete(int id)  
        {
            if (id <= 0) return BadRequest();
            Team exsist = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (exsist == null) return BadRequest();
            exsist.Image.DelteAsyncc(_env.WebRootPath, "assets", "img", "team");
            _context.Teams.Remove (exsist);
            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index));
        }

    }
}
