using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class NotificationDTO
    {
        public DateTime DateTime { get; set; }
        public virtual NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public virtual GigDTO Gig { get; set; }
    }
}
