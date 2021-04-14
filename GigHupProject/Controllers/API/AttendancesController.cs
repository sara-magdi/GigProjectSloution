using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DAL.Identity;
using DAL.DTOs;

namespace GigHubProject.Controllers.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly GigHubDbContext _context;
        private readonly UserManager<User> _userManager;

        public AttendancesController(GigHubDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        // [Route("Attendances/Attend/")]
        [HttpPost("[action]")]
        public ActionResult<Attendance> Attend(AttendanceDTO dto)
        {
            var UserId = _userManager.GetUserId(User);

            var exists = _context.Attendances.Any(a => a.AttendeeId == UserId && a.GigId == dto.GigId);
            if (exists) return BadRequest("Already Exists");


            var attendence = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = UserId
            };
            _context.Attendances.Add(attendence);
            _context.SaveChanges();


            return Ok("Great, waiting for you to attend");

        }
        //[HttpDelete]
        // [Route("Attendances/DeleteAttendance")]
        [HttpPost("[action]/{id}")]
        public ActionResult<Attendance> DeleteAttendance(int id)
        {
            var UserId = _userManager.GetUserId(User);

            var Attendance = _context
                .Attendances
                .SingleOrDefault(e => e.AttendeeId == UserId && e.GigId == id);
            
            if (Attendance == null)
                return NotFound();

            _context.Attendances.Remove(Attendance);

            _context.SaveChanges();
            return Ok(id);
        }
    }
}
