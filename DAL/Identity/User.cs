using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Identity
{
   public class User : IdentityUser<string>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }

        public User()
        {
            Followers = new HashSet<Following>();
            Followees = new HashSet<Following>();
            UserNotifications = new HashSet<UserNotification>();
        }
        public void Notify(Notification notification)
        {
            UserNotifications.Add(new UserNotification(this, notification));
        }
    }
}
