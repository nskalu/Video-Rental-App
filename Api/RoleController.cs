using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.EF;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Vidly.Api
{
    public class RoleController : ApiController
    {
        // private ApplicationDbContext db;
        //public RoleController()
        //{
        //    db = new ApplicationDbContext();
        //}
        public IEnumerable<IdentityRole> GetRoles(string query = null )
        {
            var db = new ApplicationDbContext();
            if (!string.IsNullOrEmpty(query))
            {
                var modelquery = db.Roles.Where(c => c.Name.Contains(query)).ToList();
                return modelquery;
            }
            else
            {
                var model = db.Roles.ToList();
                return model;
            }
            
        }
       

        
    }
}
