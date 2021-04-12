using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHubProject.ViewModels
{
    public class GigDetailsViewModel
    {
        public virtual Gig Gig { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
    }
}
