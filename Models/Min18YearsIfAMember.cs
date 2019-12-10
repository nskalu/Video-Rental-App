using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.EF;

namespace Vidly.Models
{
    public class Min18YearsIfAMember: ValidationAttribute
    {
        //we need to override the IsValid method
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //check the value contained in a customer membership type, make it to be of type "Customer" i.e casting it to type customer
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.Membership_Type=="Select Membership Type" ||customer.Membership_Type == "Pay As You Go")
                return ValidationResult.Success;

            if (customer.DOB == null)
                return new ValidationResult("Date of Birth is required");

            var age = DateTime.Today.Year - customer.DOB.Value.Year;
            if (age >= 18)
                return ValidationResult.Success;
            else
                return new ValidationResult("Customer must be at least 18years to go on a membership");     
            //return base.IsValid(value, validationContext);
        }  
    }
}