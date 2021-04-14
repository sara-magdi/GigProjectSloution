using DAL;
using DAL.DTOs;
using DAL.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHubProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FollowingsController : ControllerBase
    {
        private readonly GigHubDbContext _context;
        private readonly UserManager<User> _userManager;

        public FollowingsController(GigHubDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost("[action]")]
        public ActionResult<Following> Follow(FollowingDTO dto)
        {
            var UserId = _userManager.GetUserId(User);

            var exists = _context.Followings.Any(a => a.FollowerId == UserId && a.FolloweeId == dto.FolloweeId);
            if (exists) return BadRequest("Already Exists");

            var following = new Following
            {
              FollowerId = UserId ,
              FolloweeId = dto.FolloweeId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok("Great, waiting for you to follow");
        }
        [HttpPost("[action]/{id}")]
        public ActionResult<Following> Unfollow(string id)
        {
            var UserId = _userManager.GetUserId(User);
            var following = _context
                .Followings
                .SingleOrDefault(e => e.FollowerId == UserId && e.FolloweeId == id);
            if (following == null)
                return NotFound();
            _context.Followings.Remove(following);
            _context.SaveChanges();
            return Ok(id);
        }
    }
}
