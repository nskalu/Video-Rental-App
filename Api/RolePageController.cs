using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.EF;
using Vidly.Models;

namespace Vidly.Api
{
    public class RolePageController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Create(RolePage model)
        {
            var db = new VidlyDbFirstEntities1();
            //the linq below would translate to //select * from productstable where Id in(value1, value2, value3)
            var pages = db.Pages.Where(m => model.pageIds.Contains(m.Id));
            foreach (var page in pages)
            {
                var vm = new PageRole
                {
                    RoleId = model.roleId,
                    PageID = page.Id
                };
                db.PageRoles.Add(vm);
            }
            db.SaveChanges();
            return Ok();
        }
    }
}
