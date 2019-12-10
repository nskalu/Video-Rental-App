using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;
using Vidly.EF;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Membership_Type { get; set; }

        //[Min18YearsIfAMember]
        public Nullable<System.DateTimeOffset> DOB { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}