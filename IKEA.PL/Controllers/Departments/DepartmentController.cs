using IKEA.BLL.Models.Departments;
using IKEA.BLL.Services.Departments;
using IKEA.PL.Models.Departments;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers.Departments
{
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _Logger;
        private readonly IWebHostEnvironment _environment;
        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _Logger = logger;
            _environment = environment;
        }
        #endregion
        #region Index
        [HttpGet] //to get the data
        // Department/Index will be the URL
        public IActionResult Index()
        {
            var Departments = _departmentService.GetAllDepartments();
            return View(Departments);
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
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);
            var message = string.Empty;
            try
            {
                var D = _departmentService.CreateDepartment(department);
                if (D > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Sorry! The Department Hasn't Been Added";
                    ModelState.AddModelError(string.Empty, message);
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(department);
                }
                else
                {
                    message = "Sorry! The Department Hasn't Been Added";
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
            var department = _departmentService.GetDepartmentsById(id.Value);
            if (department == null)
                return NotFound();
            return View(department);
        }
        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = _departmentService.GetDepartmentsById(id.Value);
            if (department == null)
                return NotFound();
            var viewModel = new DepartmentEditVM()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate
            };
            return View(viewModel);
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DepartmentEditVM VM)
        {
            if (!ModelState.IsValid)
                return View(VM);
            var message = string.Empty;
            try
            {
                var updatedDepartment = new UpdateDepartmentDto()
                {
                    Id = id,
                    Code = VM.Code,
                    Name = VM.Name,
                    Description = VM.Description,
                    CreationDate = VM.CreationDate
                };
                var D = _departmentService.UpdateDepartmet(updatedDepartment);
                if (D > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
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
        #region Get
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = _departmentService.GetDepartmentsById(id.Value);
            if (department == null)
                return NotFound();
            return View(department);
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var D = _departmentService.DeleteDepartment(id);
                if (D)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
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
