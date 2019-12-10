using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.EF;

namespace Vidly.Api
{
    public class PageController : ApiController
    {
        //GET Pages
        public IEnumerable<Page> GetPages(string query = null)
        {
            var db = new VidlyDbFirstEntities1();
            if (!string.IsNullOrEmpty(query))
            {
                var model = db.Pages.Where(c => c.Name.Contains(query));
                return model;
            }
            else
            {
                var model = db.Pages.ToList();
                return model;
            }
           
            
        }
    }
}
