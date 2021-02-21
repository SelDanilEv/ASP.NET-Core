using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personnel_App.Infrastructure.Dto
{
    public class ActiveEmployeeDto : BasicDto
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public double Salary { get; set; }
        public string DepType { get; set; }
        public string Position { get; set; }
        public string Level { get; set; }
    }
}
