using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.EF;
using System.IO;
using System.Drawing;

namespace Vidly.Controllers
{
    public class ImagesController : Controller
    {
        // GET: Images
        public ActionResult CustomerImage(int Id)
        {
            VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
            var customer = db.Customers.SingleOrDefault(c => c.Id == Id);
            MemoryStream ms = new MemoryStream(customer.Image);
            return new FileStreamResult(ms, customer.Content_Type);
        }
    }
}