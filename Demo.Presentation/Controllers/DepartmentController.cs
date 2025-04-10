using Demo.BLL.DataTransFerObjects;
using Demo.BLL.DataTransFerObjects.DepartmentDtos;
using Demo.BLL.Services.Interfaces;
using Demo.Presentation.ViewModels.DepartmentsViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{

    public class DepartmentController(IDepartmentService _departmentService, 
    ILogger<DepartmentController> _logger,
    IWebHostEnvironment _environment) : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            //ViewData["message"] = "Hello from view Data";
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        #region Create Department
        public IActionResult Create() => View();



        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    var departmentDto = new CreatedDepartementDto()
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        DateOfCreation = departmentViewModel.DateOfCreation,
                        Description = departmentViewModel.Description,

                    };

                    int Result = _departmentService.CreateDepartment(departmentDto);
                    string Message;

                    if (Result > 0)

                        Message = $"Department {departmentViewModel.Name} Is Created Successfully";
                    else

                        Message = $"Department {departmentViewModel.Name} Can Not Be Created";

                    TempData["Message"] = Message;

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {

                        ModelState.AddModelError(key: string.Empty, errorMessage: ex.Message);
                        return View(departmentViewModel);
                    }
                    else
                    {

                        _logger.LogError(message: ex.Message);
                        return View(departmentViewModel);
                    }
                }
            }
            else
            {
                return View(departmentViewModel);
            }


        }


        #endregion

        #region Details of Departments

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();

            return View(department);
        }


        #endregion

        #region Edit Department
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentViewModel()
            {
                Code = department.code,
                Name = department.Name,
                Description = department.Description,
                //DateOfCreation = department.DateOfcreatio
            };
            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var UpdatedDepartment = new UpdatedDepartementDto()
                {
                    Code = viewModel.Code,
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    DateOfCreaction = viewModel.DateOfCreation
                };

              var Resulr=  _departmentService.UpdateDepartment(UpdatedDepartment);
                if (Resulr > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department is not Updated");
                    return View(viewModel);

                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(viewModel);
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }

        }

        #endregion

        #region Delete Department


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null) return NotFound();

            return View(department);
        }


        #endregion

    }
}
