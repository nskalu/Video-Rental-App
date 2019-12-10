using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.EF;
using Vidly.Dtos;
using AutoMapper;
using Vidly.Models;

namespace Vidly.Api
{
    public class CustomersController : ApiController
    {
        VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
        
        //GET api/customers
        public IEnumerable<CustomerDto> GetCustomers(string query = null)
        {
            db.Configuration.LazyLoadingEnabled = false;                        
            var customers = db.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return customers;
        }

        //GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var customer = db.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound);

            //db.Configuration.LazyLoadingEnabled = true;
            return Ok (Mapper.Map<Customer, CustomerDto>(customer));
        }

        
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            var addcustomer = db.Customers.Add(customer);
            db.SaveChanges();
            //to return the database generated Id to the client
            customerDto.Id = customer.Id;
            //return the Uri of the newly creared resource to the client
            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
        }

        [HttpPut] ////POST /api/customers/1
        public void UpdateCustomer(CustomerDto customerDto, int Id)
        {
            
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDB = db.Customers.SingleOrDefault(c => c.Id == Id);
            if (customerInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDB);
            db.SaveChanges();
           
        }

        [HttpDelete] // Delete /api/customers/1
        public void DeleteCustomer(int Id)
        {
            var customer = db.Customers.SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            db.Customers.Remove(customer);
            db.SaveChanges();
            
        }

    }

}
