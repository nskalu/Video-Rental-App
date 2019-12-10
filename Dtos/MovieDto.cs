using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int Qty_In_Stock { get; set; }
        public Nullable<System.DateTimeOffset> Release_Date { get; set; }

    }
}