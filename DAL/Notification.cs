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
    }
}
