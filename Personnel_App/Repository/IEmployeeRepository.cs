using Personnel_App.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personnel_App.Repository
{
    interface IEmployeeRepository
    {
        Task<List<EmployeeDto>> GetAllEmployees();

        Task<List<EmployeeDto>> GetActiveEmployees();

        Task<EmployeeDto> GetEmployee(int id);

        Task RemoveEmployee(int id);

        Task CreateEmployee(EmployeeDto employee);

        Task UpdateEmployee(EmployeeDto employee);
    }
}
