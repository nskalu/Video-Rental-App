using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Vidly.EF;

namespace Vidly.Models
{
    public class RoleName
    {
        public const string CanManageMovies = "CanManageMovies";

    }

   

    public class AssignRoleViewModel
    {
        

        public List<IdentityRole> Roles { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public string SelectedRole { get; set; }
        public string SelectedUser { get; set; }
        public List<Page> Pages { get; set; }
        public IdentityRole Role { get; set; }
        public List<string> ListOfPages { get; set; }
        public PageRole PageRoles { get; set; }
    }
    public class AssignRolePageViewModel
    {
        public string RoleId { get; set; }
        public int PageId { get; set; }
    }
}