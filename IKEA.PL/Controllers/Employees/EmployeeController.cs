using AutoMapper;
using IKEA.BLL.Models.Departments;
using IKEA.BLL.Models.Employees;
using IKEA.BLL.Services.Departments;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Models.Departments;
using IKEA.PL.Controllers.Departments;
using IKEA.PL.Models.Departments;
using IKEA.PL.Models.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers.Employees
{
    [Authorize]
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _emloyeeService;
        private readonly ILogger<EmployeeController> _Logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment,IMapper mapper)
        {
            _emloyeeService = employeeService;
            _Logger = logger;
            _environment = environment;
            _mapper = mapper;
        }
        #endregion
        #region Index
        [HttpGet] // Employee/Index [URL]
        public async Task<IActionResult> Index(string search)
        {
            ViewData["Message"] = "Hello In The Employees Page";
            ViewBag.Message = "Hello In The Employees Page[ViewBag]";
            var employees = await _emloyeeService.GetEmployeesAsync(search);
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
        public async Task<IActionResult> Create(EmployeeEditVM employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);
            var message = string.Empty;
            try
            {
                var createdEmployee = _mapper.Map<CreatedEmployeeDto>(employeeVM);
                var D = await _emloyeeService.CreateEmployeeAsync(createdEmployee);
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
                    return View(employeeVM);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employeeVM);
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee =await _emloyeeService.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
                return NotFound();
            return View(employee);
        }
        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee =await _emloyeeService.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
                return NotFound();
            var employeeVM = _mapper.Map<EmployeeDetailsDto, EmployeeEditVM>(employee);
            return View(employeeVM);
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeEditVM VM)
        {
            if (!ModelState.IsValid)
                return View(VM);
            var message = string.Empty;
            try
            {
                var updatedEmployee = _mapper.Map<UpdatedEmployeeDto>(VM);
                var D = await _emloyeeService.UpdateEmployeeAsync(updatedEmployee);
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
                    return View(VM);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Sorry! An Error Occured While Updating";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(VM);
        }
        #endregion
        #endregion
        #region Delete
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var D = await _emloyeeService.DeleteEmployeeAsync(id);
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
