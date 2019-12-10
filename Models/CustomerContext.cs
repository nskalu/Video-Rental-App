using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.EF
{
    [MetadataType(typeof(CustomerMetadataType))]
    public partial class Customer
    {
        //public List<Membership_Type> MembershipTypes { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }

    }

    public class CustomerMetadataType
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="Membership Type")]
        public string Membership_Type { get; set; }   
  
        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public Nullable<System.DateTimeOffset> DOB { get; set; }

        [FileExtensions (ErrorMessage="Pls specify an image in the formats specified")]
        public string ImageUrl { get; set; }

        [FileValidation( "png,jpg,jpeg","15360")]
        public HttpPostedFileBase PostedFile { get; set; }
    }


    public class MovieMetadataType
    {
        [Required (ErrorMessage="Name field is required")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Genre field is required")]
        public string Genre { get; set; }

        [Required (ErrorMessage = "Qty field is required")] 
        [Range(1, 200, ErrorMessage="The field quantity in stock must be between 1 and 200")]
        public string Qty_In_Stock { get; set; }

        [Required (ErrorMessage = "Release date field is required")]
        public Nullable<System.DateTimeOffset> Release_Date { get; set; }
    }

    [MetadataType(typeof(MovieMetadataType))]
    public partial class Movy
    {
        //public List<Membership_Type> MembershipTypes { get; set; }

    }

[MetadataType(typeof(NewRentalMetadataType))]
    public partial class Rental
    {

    }


    public class NewRentalMetadataType
    {
        [Required]
        [Display(Name = "Customer Name")]
        public int Customer_Id { get; set; }

        [Required]
        [Display(Name = "Movie to Rent")]
        public List<int> Movie_Id { get; set; }

        
    }
}