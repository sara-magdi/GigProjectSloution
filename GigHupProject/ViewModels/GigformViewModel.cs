using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GigHubProject.ViewModels
{
    public class GigformViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }


        public string Heading { get; set; }
        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0},{1}",Date,Time));
        }
    }
}
