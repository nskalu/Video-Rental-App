using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidly.Models
{
   

public class FileValidationAttribute:ValidationAttribute
    {
        private List<string> _validTypes;
        private string _allowedTypes;
        private string _validSize;
        private string errorMessage;
        public FileValidationAttribute( string validTypes,string validSize)
        {
            this._allowedTypes = validTypes;
            this._validTypes= validTypes.Split(',').Select(s => s.Trim().ToLower()).ToList<string>();
            this._validSize = validSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           HttpPostedFileBase file = value as HttpPostedFileBase;
            if (file != null)
            {
                    if (file != null && !_validTypes.Any(e => file.FileName.EndsWith(e)))
                    {
                    errorMessage = string.Format("Invalid file with extension uploaded. Valid file types are {0} .", _allowedTypes.ToUpper());
                        return new ValidationResult(errorMessage);
                    }
                    if (file != null && file.ContentLength > int.Parse(_validSize))
                    {
                        errorMessage =string.Format( "Invalid file with large size uploaded. Must be less than {0} bytes.",_validSize);
                        return new ValidationResult(errorMessage);
                    }
            }
            return ValidationResult.Success;
        }
        
    }
}
