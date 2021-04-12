using DAL;
using DAL.Identity;
using GigHubProject.ViewModels;
using GigHupProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GigHupProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly GigHubDbContext _context;

        public HomeController(GigHubDbContext context,ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index(string query = null)
        {
            var UserId = _userManager.GetUserId(User);
            var x = _signInManager.IsSignedIn(User);
            var upComingGigs = _context.Gigs
                .Include(e => e.Artist)
                .Include(e => e.Genre)
                .Where(e => e.DateTime > DateTime.Now && !e.IsCanceled).ToList();
            if (!string.IsNullOrWhiteSpace(query))
            {
                upComingGigs = upComingGigs
                    .Where(e =>
                e.Artist.Name.Contains(query) ||
                e.Genre.Name.Contains(query) ||
                e.Venue.Contains(query)).ToList();
            }
            var attendence = _context
                .Attendances
                .Where(e => e.AttendeeId == UserId && e.Gig.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(e => e.GigId);
            var viewModel = new GigsViewModel
            {
                UpComingGigs = upComingGigs,
                ShowAction = x,
                Heading = "Gigs I'm Going",
                SeaechTerm = query,
                Attendances = attendence
            };
            return View("Gigs", viewModel);
        }

        public IActionResult Index1()
        {
            return View();
        }
    }
}
