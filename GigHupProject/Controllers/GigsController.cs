using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using GigHubProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DAL.Identity;

namespace GigHubProject.Controllers
{
    public class GigsController : Controller
    {
        private readonly GigHubDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public GigsController(GigHubDbContext context
            , UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public IActionResult Mine()
        {
            var UserId = _userManager.GetUserId(User);
            var gig = _context.Gigs
                  .Include(e => e.Attendances)
                .ThenInclude(e => e.Attendee)
                .Where(e => e.ArtistId == UserId
                && e.DateTime > DateTime.Now
                && !e.IsCanceled)
                .Include(e => e.Genre)
                .ToList();
            return View(gig);
        }

        [Authorize]
        public IActionResult Attending()
        {
            var UserId = _userManager.GetUserId(User);
            var x = _signInManager.IsSignedIn(User);
            var gigs = _context.Attendances
                .Where(e => e.AttendeeId == UserId)
                .Include(e => e.Gig)
                    .ThenInclude(e => e.Genre)
                .Include(e => e.Gig)
                    .ThenInclude(e => e.Artist)
                .Select(a => a.Gig)
                .ToList();
            var attendence = _context
               .Attendances
               .Where(e => e.AttendeeId == UserId && e.Gig.DateTime > DateTime.Now)
               .ToList()
               .ToLookup(e => e.GigId);
            var viewModel = new GigsViewModel
            {
                UpComingGigs = gigs,
                ShowAction = x,
                Heading = "UpComing Gigs",
                Attendances = attendence
            };
            return View("Gigs", viewModel);
        }

        [Authorize]
        public IActionResult Following()
        {
            var UserId = _userManager.GetUserId(User);
            var x = _signInManager.IsSignedIn(User);
            var folow = _context.Followings
                .Include(e => e.Follower)
                .Include(e=>e.Followee)
                .Where(e => e.FollowerId == UserId);

            var viewModel = new GigsViewModel
            {
                Follow = folow,
                ShowAction = x,
                Heading = "Artists I'm Following"

            };
            return View("Followings", viewModel);
        }
        [HttpPost]
        public IActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SeaechTerm });
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gig = await _context.Gigs
                .Include(e => e.Artist)
                .Include(e => e.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            var viewModel = new GigDetailsViewModel
            {
                Gig = gig
            };
            //var UserIsAuthaticat = _signInManager.IsSignedIn(User);

            if (User.Identity.IsAuthenticated)
            {
                var UserId = _userManager.GetUserId(User);
                viewModel.IsAttending = _context
                    .Attendances
                    .Any(e => e.GigId == id && e.AttendeeId == UserId);
                viewModel.IsFollowing = _context
                    .Followings
                    .Any(e => e.FollowerId == UserId && e.FolloweeId == gig.ArtistId);
            }
            if (gig == null)
            {
                return NotFound();
            }

            return View("Details", viewModel);
        }
        // GET: Gigs
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Gigs.ToListAsync());
        }

        // GET: Gigs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var gig = await _context.Gigs
        //        .Include(g => g.Artist)
        //        .Include(g => g.Genre)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (gig == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(gig);
        //}

        // GET: Gigs/Create
        //[Authorize]
        public IActionResult Create()
        {
            var viewModel = new GigformViewModel
            {
                Heading = "Add a Gig",
                Genres = _context.Genres.ToList()
            };
            ViewData["GenreId"] = new SelectList(_context.Genres.ToList(), "Id", "Name");
            return View("GigForm", viewModel);
        }

        // POST: Gigs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GigformViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();

                return View("GigForm", viewModel);
            }


            var gig = new Gig
            {
                ArtistId = _userManager.GetUserId(User),
                DateTime = viewModel.GetDateTime(),
                Venue = viewModel.Venue,
                GenreId = viewModel.Genre
            };
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");

            if (ModelState.IsValid)
            {
                _context.Add(gig);
                await _context.SaveChangesAsync();
               // return RedirectToAction(nameof(Index));
            }
            return View("Mine","Gigs");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var UserId = _userManager.GetUserId(User);
            var gig = _context.Gigs
                .Include(e=>e.Genre)
                .Single(e => e.Id == id && e.ArtistId == UserId);
            var ViewModel = new GigformViewModel
            {
                Id = gig.Id,
                Heading = "Edit a Gig",
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("yyyy-MM-d"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };
            ViewData["GenreId"] = new SelectList(_context.Genres.ToList(), "Id", "Name");
            return View("GigForm", ViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(GigformViewModel viewModel) 
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
            var UserId = _userManager.GetUserId(User);
            var gig = _context.Gigs
                .Include(e => e.Attendances)
                .ThenInclude(e => e.Attendee)
                .Single(e => e.Id == viewModel.Id && e.ArtistId == UserId);


           // gig.Modify(viewModel.GetDateTime(), viewModel.Genre, viewModel.Venue);
            ViewData["GenreId"] = new SelectList(_context.Genres.ToList(), "Id", "Name");

            await _context.SaveChangesAsync();
            return RedirectToAction("Mine", "Gigs");
        }
        // GET: Gigs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gig = await _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gig == null)
            {
                return NotFound();
            }

            return View(gig);
        }

        // POST: Gigs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gig = await _context.Gigs.FindAsync(id);
            _context.Gigs.Remove(gig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GigExists(int id)
        {
            return _context.Gigs.Any(e => e.Id == id);
        }
    }
}
