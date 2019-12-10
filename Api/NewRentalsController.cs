using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.EF;
using AutoMapper;
using Vidly.Dtos;
using System.Data.Entity;


namespace Vidly.Api
{
    public class NewRentalsController : ApiController
    {
        VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
        //create rental api/rentals/id
         
        
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            try
            {
                
                if (!ModelState.IsValid)
                    return BadRequest();
                var customer = db.Customers.Single(c => c.Id == newRental.customerId);
                //the linq below would translate to //select * from moviestable where Id in(value1, value2, value3)
                var movies = db.Movies.Where(m => newRental.movieIds.Contains(m.Id));

                foreach (var movie in movies)
                {
                    if (movie.NoAvailable == 0)
                        return BadRequest("there are no available movies");
                    //decrement the numberAvailable in the movies table
                    movie.NoAvailable--;
                    //create a new rental object in db
                    var rental = new Rental
                    {

                        Customer_Id = customer.Id,
                        Movie_Id = movie.Id,
                        Date_Rented = DateTime.Now

                    };
                    db.Rentals.Add(rental);

                }

                db.SaveChanges();
                return Ok();
            }
            catch (System.Exception ex)
            {
                var msg = ex.Message;
                
            }
            return BadRequest("Unable to Save");
           
            
        }
    }
}
