using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.EF;

namespace Vidly.Dtos
{
    public class NewRentalDto
    {
        
        public int customerId { get; set; }
        public List<int> movieIds { get; set; }
       
    }
}