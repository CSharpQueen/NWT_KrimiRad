using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DatumUnosa : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            if (dt < DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Datum unosa ne može biti veći od današnjeg datuma");
        }

    }
}
