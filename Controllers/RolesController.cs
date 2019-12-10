using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Vidly.Models;
using Vidly.EF;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity.Validation;


namespace Vidly.Controllers
{
    public class RolesController : Controller
    {
        
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Roles
        public static IEnumerable<IdentityRole> Index(string query)
        {
            var db = new ApplicationDbContext();
            var model = db.Roles.ToList().Where(c=>c.Name.Contains(query));
            return model;
        }
        //GET Pages
        public static IEnumerable<Page> GetPages(string query)
        {
            var db = new VidlyDbFirstEntities1();
            var model = db.Pages.ToList().Where(c => c.Name.Contains(query));
            return model;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(FormCollection form)
        {
            var db = new ApplicationDbContext(); //opens a connection to the database   
            
            var roleStore = new RoleStore<IdentityRole>(db); // RoleStore is a generic class that stores roles, we used asp.net IdentityRole type class cos our application doesnt have one
            var roleManager = new RoleManager<IdentityRole>(roleStore); //we create a RoleManager class in our rolestor, its the rolemanager class that allows us create, delete,edit roles etc         
            await roleManager.CreateAsync(new IdentityRole(form["txtName"])); //this is where it saves to db. as in a rolestore requires a rolemanager          
            ViewBag.Message = "Role successfully created";
            return View();
        }

        [HttpGet]
        public ActionResult AssignUserToRole()
        {
            var db = new ApplicationDbContext();
            var roles = db.Roles.ToList();
            var users = db.Users.ToList();            
            AssignRoleViewModel model = new AssignRoleViewModel();           
            model.Users = users;
            model.Roles = roles;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AssignUserToRole(AssignRoleViewModel model)
        {
            var db = new ApplicationDbContext();
            try
            {
                  
            
                await UserManager.AddToRoleAsync(model.SelectedUser, model.SelectedRole);
            }

            catch(DbEntityValidationException ex)
            {
                ViewBag.Message = ex.Message;
            }
            //to load the page again requires the below code to populate the dropdowns
            ViewBag.Message = "Role successfully assigned";
            var users = db.Users.ToList();
            var roles = db.Roles.ToList();
            model.Users = users;
            model.Roles = roles;
            
            return View(model);
        }

        public ActionResult AssignRoleToPage()
        {

            var db = new ApplicationDbContext();
            var dbs = new VidlyDbFirstEntities1();
            var pages = dbs.Pages.ToList();
            var roles = db.Roles.ToList();
            AssignRoleViewModel model = new AssignRoleViewModel();
            model.Pages = pages;
            model.Roles = roles;

            return View(model);
        }
        [HttpPost]
        public ActionResult AssignRoleToPage(PageRole model, AssignRoleViewModel pgr)
        {
            var db = new VidlyDbFirstEntities1();
            //next line is to get at the items in the list i have on my view, thats what i am yet to achieve 

            foreach (var item in pgr.ListOfPages)
            {
               var page = db.Pages.SingleOrDefault(c => c.Name == item);
                var pg = new PageRole
                {
                    RoleId= model.RoleId,
                    PageID= page.Id
                    
                };
                

                db.PageRoles.Add(pg);
            }

            return View();
        }


        
    }
}