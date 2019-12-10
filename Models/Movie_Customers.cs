using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.EF;

namespace Vidly.EF
{
    public partial class VidlyDbFirstEntities1
    {
        public List<vwRental> Customer_List { get; set; }
        public Movy Movie { get; set; }
        public vwRental Movie_Rented { get; set; }
    }
}