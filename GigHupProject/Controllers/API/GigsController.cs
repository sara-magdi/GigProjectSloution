using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Identity;
using Microsoft.AspNetCore.Identity;

namespace GigHubProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GigsController : ControllerBase
    {
        private readonly GigHubDbContext _context;
        private readonly UserManager<User> _userManager;

        public GigsController(GigHubDbContext context,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("{id}")]
        public ActionResult Cancel(int id)
        {
            var UserId = _userManager.GetUserId(User);
            var gig = _context.Gigs
                .Include(e => e.Attendances)
                .ThenInclude(e => e.Attendee)
                .Single(e => e.Id == id && e.ArtistId == UserId);

            if (gig.IsCanceled)
                return NotFound();

            gig.Cancel();

            _context.SaveChanges();
            return Ok("succes");
        }
    }
}
