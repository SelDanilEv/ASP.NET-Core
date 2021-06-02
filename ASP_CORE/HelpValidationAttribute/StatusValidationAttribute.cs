using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_CORE.HelpValidationAttribute
{
    public class StatusValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if ((string)value != "active" && (string)value != "passive" )
            {
                this.ErrorMessage = "Error status";
                return false;
            }
            return true;
        }
    }
}
