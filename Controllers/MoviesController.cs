using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;
using Vidly.ViewModels;
using Vidly.EF;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            var movies = new List<Movie>{ 
                new Movie{ Name = "Shrek", Id=1}, 
                new Movie { Name = "Juraissic Park", Id=2}};           
            var viewModel = new RandomMovieViewModel { Movie_List = movies};
            return View(viewModel);
        }

        // GET: Movies by id
        public ActionResult Detail()
        {
            var movie = new List<Movie> {
                new Movie {Name = "Shrek", Id=1 },
                new Movie {Name = "Jurraisic Park", Id=2 },
            new Movie {Name = "Zee World", Id=2 }};           
            var customers = new List<CustomerVM> {
                new CustomerVM{Name="Ngozi Kalu"}, 
                new CustomerVM{Name="John Smith"},
            new CustomerVM{Name="Onyedikachi Awaji"}};
            //create an instance of our viewModel
            var viewModel = new RandomMovieViewModel { Movie_List=movie, Customer=customers };
            return View(viewModel);
        }


        //using optional parameters, i.e if a parameter is not specified
        public ActionResult Index1(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
            
        }


        public ActionResult Index2()
        {
            VidlyDbFirstEntities1 dbContext = new VidlyDbFirstEntities1();
            List<Movy> movies = dbContext.Movies.ToList();
            //check if the user is in the roles table for the role specified. RoleName is a class we defined
           
            if (User.IsInRole(RoleName.CanManageMovies))
                return View(movies);
            return View("ReadOnlyIndex", movies);
            
        }

        public ActionResult Details(int Id)
        {
            VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
            //Movy movie = db.Movies.SingleOrDefault(m => m.Id == Id);
            vwRental movieRented = db.vwRentals.FirstOrDefault(m => m.MovieId == Id);
            List<vwRental> vwrental = db.vwRentals.ToList();
            var moviecust = new VidlyDbFirstEntities1 { Customer_List = vwrental, Movie_Rented = movieRented, };
            return View(moviecust);
        }

        //[Authorize(Roles=RoleName.CanManageMovies)]
        public ActionResult Create() 
        {
            string viewName = "Movies_Create";
            VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
            var pageId = db.Pages.SingleOrDefault(c => c.Name == viewName);//use the viewname i passed to retrieve the view id
            var pagerole = db.PageRoles.ToList().Where(c => c.PageID == pageId.Id);//used the viewid i retrieved to select records from db that relate to that id
            
            foreach (var role in pagerole) //checks thru each retrieved record of roles ids
            {
                var dbs = new ApplicationDbContext();
                var roles = dbs.Roles.SingleOrDefault(c=>c.Id==role.RoleId);// i used the roleId to get the rolename from applicationdbcontext cos i need to pass the name as string 
                if (User.IsInRole(roles.Name))
                {
                    if (!(ViewData["movie"] == null))
                    {
                        return View((Movy)ViewData["movie"]);
                    }

                    var genre = db.Genres.ToList();
                    var viewmodel = new MovieGenre { MovieGenres = genre };
                    return View(viewmodel);

                }
                //return View();
            }
            return RedirectToAction("AccessDenied", "Movies");
            
        }

        [HttpPost]
        public ActionResult Create(Movy movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    VidlyDbFirstEntities1 dbsave = new VidlyDbFirstEntities1();
                    // had to set my values like this becos i want to set the value of my noAvailable property whenever a new movie is added
                    var mov = new Movy
                    {
                        Name = movie.Name,
                        Release_Date = movie.Release_Date,
                        Genre = movie.Genre,
                        Qty_In_Stock = movie.Qty_In_Stock,
                        NoAvailable = movie.Qty_In_Stock
                    };
                    var save = dbsave.Movies.Add(mov);
                    dbsave.SaveChanges();
                    return RedirectToAction("index2", "Movies");
                }
                
                
            }
            catch (System.Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            ViewData["movie"] = movie;
            VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
            var genre = db.Genres.ToList();
            var viewmodel = new MovieGenre { MovieGenres = genre, Movie=movie };      
            return View("Create", viewmodel);

            
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int Id)
        {

            VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
            var retMovie = db.Movies.SingleOrDefault(m => m.Id == Id);
            if (retMovie == null)
                return Content("No movie with this Id");
            var viewModel = new MovieGenre
            {
                Movie = retMovie,
                MovieGenres = db.Genres.ToList()
            };
            return View("Edit", viewModel);
            
        }

        [HttpPost]
        public ActionResult Edit(Movy movie)
        {
            VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
            var retMovie = db.Movies.SingleOrDefault(m => m.Id == movie.Id);
            retMovie.Id = movie.Id;
            retMovie.Name = movie.Name;
            retMovie.Release_Date = movie.Release_Date;
            retMovie.Genre = movie.Genre;
            retMovie.Qty_In_Stock = movie.Qty_In_Stock;
            db.SaveChanges();
            return RedirectToAction("Index2", "Movies");
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}