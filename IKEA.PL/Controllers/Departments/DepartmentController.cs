using AutoMapper;
using IKEA.BLL.Models.Departments;
using IKEA.BLL.Services.Departments;
using IKEA.PL.Models.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers.Departments
{
    //[AllowAnonymous] //to allow all users to access the controller
    [Authorize] //to allow only authenticated users to access the controller
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _Logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment,IMapper mapper)
        {
            _departmentService = departmentService;
            _Logger = logger;
            _environment = environment;
            _mapper = mapper;
        }
        #endregion
        #region Index
        [HttpGet] //to get the data
        // Department/Index will be the URL
        public async Task<IActionResult> Index()
        {
            ViewData["Message"] = "Hello In The Departments Page";
            ViewBag.Message = "Hello In The Departments Page[ViewBag]";
            var Departments = await _departmentService.GetAllDepartmentsAsync();
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
        public async Task<IActionResult> Create(DepartmentEditVM departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);
            var message = string.Empty;
            try
            {
                //var D = _departmentService.CreateDepartment(new CreatedDepartmentDto()
                //{
                //    Code = department.Code,
                //    Name = department.Name,
                //    Description = department.Description,
                //    CreationDate = department.CreationDate
                //});
                var createdDepartment = _mapper.Map<CreatedDepartmentDto>(departmentVM);
                var D = await _departmentService.CreateDepartmentAsync(createdDepartment);
                if (D > 0)
                {
                    TempData["Message"]= "The Department Has Been Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Sorry! The Department Hasn't Been Created";
                    message = "Sorry! The Department Hasn't Been Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentVM);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentVM);
                }
                else
                {
                    message = "Sorry! The Department Hasn't Been Created";
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
            var department = await _departmentService.GetDepartmentsByIdAsync(id.Value);
            if (department == null)
                return NotFound();
            return View(department);
        }
        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = await _departmentService.GetDepartmentsByIdAsync(id.Value);
            if (department == null)
                return NotFound();
            var departmentVM = _mapper.Map<DepartmentsDetailsReturnDto,DepartmentEditVM>(department);
            return View(departmentVM);
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentEditVM VM)
        {
            if (!ModelState.IsValid)
                return View(VM);
            var message = string.Empty;
            try
            {
                //var updatedDepartment = new UpdateDepartmentDto()
                //{
                //    Id = id,
                //    Code = VM.Code,
                //    Name = VM.Name,
                //    Description = VM.Description,
                //    CreationDate = VM.CreationDate
                //};
                var updatedDepartment = _mapper.Map< UpdateDepartmentDto>(VM);
                var D = await _departmentService.UpdateDepartmetAsync(updatedDepartment);
                if (D > 0)
                {
                    TempData["Message"] = "The Department Has Been Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Sorry! The Department Hasn't Been Updated";
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = await _departmentService.GetDepartmentsByIdAsync(id.Value);
            if (department == null)
                return NotFound();
            return View(department);
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var D = await _departmentService.DeleteDepartmentAsync(id);
                if (D)
                {
                    TempData["Message"] = "The Department Has Been Deleted Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Sorry! The Department Hasn't Been Deleted";
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
