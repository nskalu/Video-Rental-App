using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.EF;

namespace Vidly.ViewModels
{
    public class MovieGenre
    {
        public List<Genre> MovieGenres { get; set; }
        public Movy Movie { get; set; }
    }
}