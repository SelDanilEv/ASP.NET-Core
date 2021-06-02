using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ASP_CORE.HelpValidationAttribute
{
    public class RoleValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            
            if ((string)value!="admin" && (string)value!= "customer"&& (string)value != "HR")
            {
                this.ErrorMessage = "Error Role";
                return false;
            }
            return true;
        }
    }
}
