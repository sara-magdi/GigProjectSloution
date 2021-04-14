
using AutoMapper;
using DAL;
using DAL.DTOs;
using DAL.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHubProject.Controllers.API
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly GigHubDbContext _cotext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public NotificationsController(GigHubDbContext cotext, UserManager<User> userManager, IMapper mapper)
        {
            _cotext = cotext;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<NotificationDTO> GetNotifications()
        {
            var UserId = _userManager.GetUserId(User);
            var notifications = _cotext.UserNotifications
                .Where(e => e.UserId == UserId && !e.IsRead)
                .Include(e => e.Notification)
                .ThenInclude(e => e.Gig)
                .ThenInclude(e => e.Artist)
                .Select(e => e.Notification)
                .ToList();


            return notifications.Select(_mapper.Map<Notification, NotificationDTO>);
        }
        [HttpPost]
        [Route("MarkAsRead")]
        public ActionResult<NotificationDTO> MarkAsRead()
        {
            var UserId = _userManager.GetUserId(User);
            var notifications = _cotext.UserNotifications
                .Where(e => e.UserId == UserId && !e.IsRead)
                .ToList();
            notifications.ForEach(e => e.Read());
            _cotext.SaveChangesAsync();
            return Ok("sucess");
        }
    }
}
