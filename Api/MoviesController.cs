using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.EF;
using AutoMapper;
using Vidly.Dtos;

namespace Vidly.Api
{
    public class MoviesController : ApiController
    {
        VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
        //GET api/movies
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            db.Configuration.LazyLoadingEnabled = false;

            // checks if the query is not empty
            if (!String.IsNullOrWhiteSpace(query))
            {
                var moviesQuery = db.Movies.Where(c => c.Name.Contains(query)).ToList().Select(Mapper.Map<Movy, MovieDto>);
                return moviesQuery;
            }
            else
            {
                var movies = db.Movies.ToList().Select(Mapper.Map<Movy, MovieDto>);
                return movies;
            }

        }

       

        //GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var movie = db.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return NotFound();
            //throw new HttpResponseException(HttpStatusCode.NotFound);

            //db.Configuration.LazyLoadingEnabled = true;
            return Ok(Mapper.Map<Movy, MovieDto>(movie));
        }

        //GET /api/movies/
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            //check for validation
            if (!ModelState.IsValid)
                return BadRequest();
            // maps the model we coming with to the domain model
            var movie = Mapper.Map<MovieDto, Movy>(movieDto);
            //this adds the above mapped model to the database
            var addcustomer = db.Movies.Add(movie);
            db.SaveChanges();
            //to return the database generated Id to the client
            movieDto.Id = movie.Id;
            //return the Uri of the newly creared resource to the client
            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        [HttpPut] ////GET /api/movies/1
        public void UpdateMovie(MovieDto movieDto, int Id)
        {

            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var movieInDB = db.Movies.SingleOrDefault(c => c.Id == Id);
            if (movieInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map<MovieDto, Movy>(movieDto, movieInDB);
            //Mapper.CreateMap<Movy, MovieDto>().ForMember(m => m.Id, opt => opt.Ignore());
            db.SaveChanges();

        }

        [HttpDelete] // Delete /api/movies/1
        public void DeleteMovie(int Id)
        {
            var movie = db.Movies.SingleOrDefault(c => c.Id == Id);
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            db.Movies.Remove(movie);
            db.SaveChanges();

        }
    }
}
