using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public EmployeesController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await applicationDbContext.Employees.ToListAsync();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Add()
        {
            List<Department> objListDepartment = new List<Department>();

            objListDepartment = (from d in applicationDbContext.Departments select d).ToList();

            objListDepartment.Insert(0, new Department { DEPARTMENT_ID = 0, DEPARTMENT_NAME = "--Select Department Name--" });

            ViewBag.message = objListDepartment;

            return View();
        }
        [HttpPost]
        public IActionResult Add(AddEmployeeViewModel addEmployeeRequest)
        {



            if (applicationDbContext.Employees.Any(i => i.Name == addEmployeeRequest.Name))
            {
                ModelState.AddModelError("Employee.DEPARTMENT_NAME", "The department is already in use.");
                ViewBag.Message1 = "This Name is already in use.";
                return View(addEmployeeRequest);
            }


            var employee = new Employee()
            {
              
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = Convert.ToInt32(addEmployeeRequest.Department),
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                   
            };
            applicationDbContext.Employees.Add(employee);
            applicationDbContext.SaveChanges();
            string message = "Created the record successfully";
            ViewBag.Message = message;

            return RedirectToAction("Index");


        }
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var employee = await applicationDbContext.Employees.FirstOrDefaultAsync(x => x.ID == id);

            if (employee != null)
            {
                var ViewModel = new UpdateEmployeeViewModel()
                {
                    ID = employee.ID,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department.ToString(),
                    DateOfBirth = employee.DateOfBirth
                };

                return await Task.Run(() => View("View", ViewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await applicationDbContext.Employees.FindAsync(model.ID);

            if (employee != null)
            {

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = Convert.ToInt32(model.Department);
                employee.DateOfBirth = model.DateOfBirth;

                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await applicationDbContext.Employees.FindAsync(model.ID);

            if (employee != null)
            {
                applicationDbContext.Employees.Remove(employee);
                 await applicationDbContext.SaveChangesAsync(); 
            }

            return RedirectToAction("Index");



        }


    }
}
