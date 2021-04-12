using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
   public class GigDTO
    {
       
            public int Id { get; set; }
            public string Venue { get; set; }
            public bool IsCanceled { get; set; }
            public virtual UserDTO Artist { get; set; }
            public DateTime DateTime { get; set; }
            public GenreDTO Genre { get; set; }
        
    }
}
