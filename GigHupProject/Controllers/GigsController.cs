using DAL;
using DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHubProject.Controllers
{
    public class GigsController : Controller
    {
        private readonly GigHubDbContext _context;
        private readonly UserManager<User> _userManager;

        public GigsController(GigHubDbContext context,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gigs.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var gig = await _context.Gigs.ToListAsync();
            ViewData["GenreId"] = new SelectList(await _context.Genres.ToListAsync(),"Id","Name");
            return View(gig);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Gig gig)
        {
            var Gig = await _context.Gigs.ToListAsync();
            ViewData["GenreId"] = new SelectList(await _context.Genres.ToListAsync(), "Id", "Name");
            _context.Add(gig);
            _context.SaveChanges();
            return View();
        }
    }
}
