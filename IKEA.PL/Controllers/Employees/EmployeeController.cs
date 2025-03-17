using IKEA.BLL.Models.Departments;
using IKEA.BLL.Models.Employees;
using IKEA.BLL.Services.Departments;
using IKEA.BLL.Services.Employees;
using IKEA.PL.Controllers.Departments;
using IKEA.PL.Models.Departments;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers.Employees
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _emloyeeService;
        private readonly ILogger<EmployeeController> _Logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            _emloyeeService = employeeService;
            _Logger = logger;
            _environment = environment;
        }
        #endregion
        #region Index
        [HttpGet] // Employee/Index [URL]
        public IActionResult Index(string search)
        {
            ViewData["Message"] = "Hello In The Employees Page";
            ViewBag.Message = "Hello In The Employees Page[ViewBag]";
            var employees = _emloyeeService.GetEmployees(search);
            return View(employees);
        }
        #endregion
        #region Create
        #region Get
        [HttpGet] //to get the view
        public IActionResult Create()
        {
            return View();
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {
                var D = _emloyeeService.CreateEmployee(employee);
                if (D > 0)
                {
                    TempData["Message"] = "The Employee Has Been Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Sorry! The Department Hasn't Been Created";
                    message = "Sorry! The Employee Hasn't Been Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employee);
                }
                else
                {
                    message = "Sorry! The Employee Hasn't Been Added";
                    return View("Error", message);

                }
            }
        }
        #endregion
        #endregion
        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee = _emloyeeService.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound();
            return View(employee);
        }
        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public IActionResult Edit(int? id, [FromServices] IDepartmentService departmentService)
        {
            if (id == null)
                return BadRequest();
            var employee = _emloyeeService.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound();
            ViewData["Departments"] = departmentService.GetAllDepartments();
            var viewModel = new UpdatedEmployeeDto()
            {
                Name = employee.Name,
                Address = employee.Address,
                Email = employee.Email,
                Age = employee.Age,
                Salary = employee.Salary,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,   
            };
            return View(viewModel);
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UpdatedEmployeeDto Updated)
        {
            if (!ModelState.IsValid)
                return View(Updated);
            var message = string.Empty;
            try
            { 
                var D = _emloyeeService.UpdateEmployee(Updated);
                if (D > 0)
                {
                    TempData["Message"] = "The Employee Has Been Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Sorry! The Employee Hasn't Been Updated";
                    message = "Sorry! An Error Occured While Updating";
                    ModelState.AddModelError(string.Empty, message);
                    return View(Updated);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Sorry! An Error Occured While Updating";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(Updated);
        }
        #endregion
        #endregion
        #region Delete
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var D = _emloyeeService.DeleteEmployee(id);
                if (D)
                {
                    TempData["Message"] = "The Employee Has Been Deleted Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Sorry! The Employee Hasn't Been Deleted";
                    message = "Sorry! An Error Occured While Deleting";
                    ModelState.AddModelError(string.Empty, message);
                    return View();
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Sorry! An Error Occured While Deleting";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #endregion
    }
}
