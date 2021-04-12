using DAL.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; private set; }
        public virtual User User { get; private set; }


        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }
        public virtual Notification Notification { get; private set; }

        public bool IsRead { get; set; }
    }
}
