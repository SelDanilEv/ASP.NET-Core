using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personnel_App.Infrastructure.Dto
{
    public class EmployeeDto : ActiveEmployeeDto
    {
        public bool IsActive { get; set; }
    }
}
