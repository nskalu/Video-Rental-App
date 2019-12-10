using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<CustomerVM> Customer { get; set; }
        public List<Movie> Movie_List { get; set; }
    }
}