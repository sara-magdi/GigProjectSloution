using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public virtual NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        [Required]
        public virtual Gig Gig { get; private set; }
        protected Notification()
        {
        }
        //Validation
        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentException("gig");
            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;

        }

        public static Notification GigCreate(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdate(Gig newgig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newgig);
            notification.OriginalVenue = originalVenue;
            notification.OriginalDateTime = originalDateTime;
            return notification;
        }
        public static Notification GigCancel(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}
