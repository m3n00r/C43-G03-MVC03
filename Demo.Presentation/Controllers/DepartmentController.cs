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
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        #region Create Department
        public IActionResult Create() => View();



        [HttpPost]
        public IActionResult Create(CreatedDepartementDto departmentDto)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    int Result = _departmentService.CreateDepartment(departmentDto);
                    if (Result > 0)
                        return View(viewName: nameof(Index));
                    else
                    {
                        return View(departmentDto);
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {

                        ModelState.AddModelError(key: string.Empty, errorMessage: ex.Message);
                        return View(model: departmentDto);
                    }
                    else
                    {

                        _logger.LogError(message: ex.Message);
                        return View(model: departmentDto);
                    }
                }
            }
            else
            {
                return View(departmentDto);
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
            var departmentViewModel = new DepartmentEditViewModel()
            {
                Code = department.code,
                Name = department.Name,
                Description = department.Description,
                //DateOfCreation = department.DateOfcreatio
            };
            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentEditViewModel viewModel)
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
