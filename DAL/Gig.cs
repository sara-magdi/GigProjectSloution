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
    }
}
