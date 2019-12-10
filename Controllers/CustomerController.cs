using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;
using Vidly.EF;
using System.IO;
using System.Data.Entity.Validation;
using System.Drawing;




namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {


            var customer = GetCustomers();
            return View(customer);
        }

        public ActionResult Detail(int Id)
        {
             //i created the list of names here initially (hardcoded), but moving it into a method offers flexibility so i did
            //var viewModel = new RandomMovieViewModel { Customer = customers (ds customers was the name i used to save my list) };
        
             
            var customer = GetCustomers().Single(cus => cus.Id == Id);
            if (customer == null)
            { 
                return HttpNotFound();
            } 
            return View(customer);
        }

        private IEnumerable<CustomerVM> GetCustomers()
        {
            return new List<CustomerVM> { 
                new CustomerVM { Name = "Ngozi Kalu", Id=1 }, 
                new CustomerVM { Name = "John Smith", Id=2 },
                new CustomerVM { Name = "Onyedikachi Awaji", Id=3 } };
        }

        public ActionResult Index1()
        {
            VidlyDbFirstEntities1 customercontext = new VidlyDbFirstEntities1();
            List<Customer> customer = customercontext.Customers.ToList();
            return View(customer);
        }

        public ActionResult Detail1(int Id)
        {
            VidlyDbFirstEntities1 dbDetails = new VidlyDbFirstEntities1();
            Customer cusDetails = dbDetails.Customers.SingleOrDefault(c => c.Id == Id);
            //retriev the imageurl from db
            cusDetails.ImageUrl = cusDetails.ImageUrl== null ? "" : cusDetails.ImageUrl.Replace(" ", "%20");
           
            
            return View(cusDetails);
        }


        public ActionResult Create()
        {
            if(!(ViewData["customer"] == null))
            {
               return View((Customer)ViewData["customer"]); 
            }
            VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
            var membershipType = db.Membership_Type.ToList();
            var viewmodel = new CustomerMembershipType { MembershipTypes = membershipType };
            return View(viewmodel);
            
        }
    
        [HttpPost]

        public ActionResult Create(Customer customer)//, HttpPostedFileBase PostedFile
        {
            
            try
            {
                
                if (ModelState.IsValid) 

                {
                   
                    
                    VidlyDbFirstEntities1 dbsave = new VidlyDbFirstEntities1();
                    //saving image to project folder and path to db

                    var fileName = Path.GetFileName(customer.PostedFile.FileName); //getting the file name and extension from the file above                    
                        var path = Path.Combine(Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "Images/", fileName);   // sets the image server path, the one with localhost/....
                        var localpath = Path.Combine(Server.MapPath("~/Images/") + fileName);  // sets the image local path, the one with c:\\users...
                        customer.ImageUrl = path; //sets the server path to imageUrl property b4 saving it in db

                        customer.PostedFile.SaveAs(localpath); //saves the image local path to the images folder in our project 
                        customer.Image = ReadFully(customer.PostedFile.InputStream);//inputStream is the method that allows you to read from the uploaded file;
                        customer.Content_Type = customer.PostedFile.ContentType;
                        var save = dbsave.Customers.Add(customer);
                        dbsave.SaveChanges();

                
                    return RedirectToAction("index1", "Customer");
                }                            
            }
            catch (DbEntityValidationException ex)
            {
               Console.WriteLine(ex);
            }
            //was trying to return the validation errors in viewdata, so i had to still use the view model here too, also to populate the membershiptype ddl
            ViewData["customer"] = customer;
            VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
            var membershipType = db.Membership_Type.ToList();
            var viewmodel = new CustomerMembershipType { MembershipTypes = membershipType, Customer=customer  };
            
            return View("Create", viewmodel);
        }

        public ActionResult Edit(int Id)
        {
            VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
            var retOneCustomer = db.Customers.SingleOrDefault(c => c.Id == Id);
            if (retOneCustomer == null)
                return Content("No Customer with this Id");
            var viewModel = new CustomerMembershipType
            {
                Customer = retOneCustomer,
                MembershipTypes = db.Membership_Type.ToList()
            };
            return View("Edit", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            VidlyDbFirstEntities1 db = new VidlyDbFirstEntities1();
            //we need to get the customer from the db first so that our dbcontext can track the changes in that entity
            var updateCustomer = db.Customers.Single(c => c.Id == customer.Id);
            updateCustomer.Id = customer.Id;
            updateCustomer.Name = customer.Name;
            updateCustomer.Membership_Type = customer.Membership_Type;
            updateCustomer.DOB = customer.DOB;
            updateCustomer.Address = customer.Address;
            updateCustomer.Phone = customer.Phone;
            db.SaveChanges();
            return RedirectToAction("Index1", "Customer");
        }


        public static byte[] ImageToBinary(string imagePath)
        {
            //this method is used if u want to read the image from the server path or local path
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0,  (int)fileStream.Length);
            fileStream.Close();
            return buffer;
        }
        public static byte[] ReadFully(Stream input)
        {
            //this method is used when u want to read an image file as u r uploding it i.e from the location it is on the computer
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        
    }
}