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
    public class Attendance
    {
        public virtual User Attendee { get; set; }
        [Column(Order = 2)]
        [Key]
        public string AttendeeId { get; set; }
        public virtual Gig Gig { get; set; }
        [Column(Order = 1)]
        [Key]
        public int GigId { get; set; }
    }
}
