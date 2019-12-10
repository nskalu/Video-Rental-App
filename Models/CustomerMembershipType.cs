using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.EF;

namespace Vidly.Models
{
    public class CustomerMembershipType
    {
        public List<Membership_Type> MembershipTypes { get; set; }
        public Customer Customer { get; set; }

    }
}