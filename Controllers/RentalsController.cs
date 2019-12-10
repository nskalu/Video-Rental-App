using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.EF;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals page
        public ActionResult Index()
        {
            //VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
            //List<Rental> rentals = db.Rentals.ToList();
            return View();
        }
    }
}