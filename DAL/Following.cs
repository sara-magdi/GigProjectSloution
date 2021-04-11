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
   public class Following
    {
        [MaxLength(450)]
        [Key]
        [Column(Order = 2)]
        public string FolloweeId { get; set; }
        public virtual User Followee { get; set; }


        [MaxLength(450)]
        [Key]
        [Column(Order = 1)]
        public string FollowerId { get; set; }
        public virtual User Follower { get; set; }

   
        
    }
}
