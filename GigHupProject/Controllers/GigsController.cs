using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace GigHubProject.Controllers
{
    public class GigsController : Controller
    {
        private readonly GigHubDbContext _context;

        public GigsController(GigHubDbContext context)
        {
            _context = context;
        }

        // GET: Gigs1
        public async Task<IActionResult> Index()
        {
            var gigHubDbContext = _context.Gigs.Include(g => g.Artist).Include(g => g.Genre);
            return View(await gigHubDbContext.ToListAsync());
        }

        // GET: Gigs1/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Gigs1/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id");
            return View();
        }

        // POST: Gigs1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Venue,DateTime,GenreId,ArtistId")] Gig gig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Users, "Id", "Id", gig.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", gig.GenreId);
            return View(gig);
        }

        // GET: Gigs1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gig = await _context.Gigs.FindAsync(id);
            if (gig == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Users, "Id", "Id", gig.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", gig.GenreId);
            return View(gig);
        }

        // POST: Gigs1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Venue,DateTime,GenreId,ArtistId")] Gig gig)
        {
            if (id != gig.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GigExists(gig.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Users, "Id", "Id", gig.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", gig.GenreId);
            return View(gig);
        }

        // GET: Gigs1/Delete/5
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
