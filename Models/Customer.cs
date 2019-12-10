using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Models
{
    [Table("Customer")]
    public class CustomerVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}