using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel_App.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Personnel_App.Repository;

namespace Personnel_App.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _config;
        private static EmployeeRepository _repository;

        public EmployeeController(IConfiguration config)
        {
            _config = config;
            _repository = new EmployeeRepository(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
        }

        public async Task<ActionResult> Index()
        {
            List<EmployeeDto> employees = await _repository.GetAllEmployees();

            return View(employees);
        }

        public async Task<ActionResult> ActiveEmployee()
        {
            List<EmployeeDto> employees = await _repository.GetActiveEmployees();

            return View("Index",employees);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeDto employee)
        {
            try
            {
                await _repository.CreateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async  Task<ActionResult> Edit(int id)
        {
            return View(await _repository.GetEmployee(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeDto employee)
        {
            try
            {
                await _repository.UpdateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _repository.RemoveEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
