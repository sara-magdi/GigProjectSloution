using DAL.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class Gig
    {
        public int Id { get; set; }
        public string Venue { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsCanceled { get; private set; }

        [Required]
        public byte GenreId { get; set; }
        public Genre Genre { get; set; }
        [Required]
        public string ArtistId { get; set; }
        public virtual User Artist { get; set; }
        public  ICollection<Attendance> Attendances { get; internal set; }
        public Gig()
        {
            Attendances = new HashSet<Attendance>();
        }
        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.GigCancel(this);
            foreach (var attendnee in Attendances.Select(e => e.Attendee))
            {
                attendnee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, byte genre, string venue)
        {
            var notification = Notification.GigUpdate(this, DateTime, Venue);

            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;
            foreach (var attendee in Attendances.Select(e => e.Attendee))
                attendee.Notify(notification);
        }
    }
}
