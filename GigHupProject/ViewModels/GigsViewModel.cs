using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHubProject.ViewModels
{
    public class GigsViewModel
    {
        public ILookup<int, Attendance> Attendances;

        public IEnumerable<Gig> UpComingGigs { get; set; }
        public IEnumerable<Following> Follow { get; set; }

        public bool ShowAction { get; set; }
        public string Heading { get; set; }
        public string SeaechTerm { get; set; }
    }
}
