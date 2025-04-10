using System;
using Demo.BLL.DataTransFerObjects.EmployeeDtos;
using Demo.BLL.Services.Interfaces;
using Demo.DLL.Models.EmployeeModel;
using Demo.DLL.Models.Shared.Enums;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService,
        IWebHostEnvironment environment ,
        ILogger<EmployeesController> _logger,
       IDepartmentService departmentService ) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }

        #region create Employee
        [HttpGet]

        public IActionResult Create([FromServices]IDepartmentService departmentService)
        {
            ViewData["Departments"] = departmentService.GetAllDepartments();
            return View();
        }

        [HttpPost]

        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    var employeeDto= new CreatedEmployeeDto() 
                    { 
                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Email = employeeViewModel.Email,
                        EmployeeType = employeeViewModel.EmployeeType,
                        Gender = employeeViewModel.Gender,
                        HiringDate = employeeViewModel.HiringDate,
                        IsActive = employeeViewModel.IsActive,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        Salary = employeeViewModel.Salary,
                        DepartmentId = employeeViewModel.DepartmentId,
                    };
                    int Result = _employeeService.CreateEmployee(employeeDto);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "Can't Create Employee");
                }
                catch (Exception ex)
                {
                    if (environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }
            }

            return View(employeeViewModel);

        }
        #endregion

        #region Details Of Employee
        [HttpGet]
        public IActionResult Details(int? id )
        {
            if (!id.HasValue) return BadRequest();

            var employee = _employeeService.GetEmployeebyId(id.Value);

            return employee is null ? NotFound() : View (employee);

        }


        #endregion

        #region Update Employee
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeebyId(id.Value);
            if (employee is null) return NotFound();

            var employeeViewModel = new EmployeeViewModel()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId,
               
            };
            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
        {
            if (!id.HasValue ) return BadRequest();
            if (!ModelState.IsValid) return View(model: employeeViewModel);

            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeViewModel.Name,
                    Salary = employeeViewModel.Salary,
                    Address = employeeViewModel.Address,
                    Age = employeeViewModel.Age,
                    Email = employeeViewModel.Email,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    IsActive = employeeViewModel.IsActive,
                    EmployeeType =employeeViewModel.EmployeeType,
                    Gender = employeeViewModel.Gender,
                    HiringDate = employeeViewModel.HiringDate,
                    DepartmentId = employeeViewModel.DepartmentId 

                };
                var Result = _employeeService.UpdateEmployee(employeeDto);
                if (Result > 0)
                {
                    return RedirectToAction(actionName: nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(key: string.Empty, errorMessage: "Employee is not Updated");
                    return View(employeeViewModel);
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeViewModel);
                }
                   
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView",ex);
                }
            }
        }

        #endregion

        #region Delete Employee
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();

            try
            {
                var Deleted = _employeeService.DeleteEmployee(id);
                if (Deleted)
                {
                    return RedirectToAction(actionName: nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(key: string.Empty, errorMessage: "Employee is not Deleted");
                    return RedirectToAction(actionName: nameof(Delete), routeValues: new { id = id });
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    return RedirectToAction(actionName: nameof(Index));
                    // With Message That Department Not Deleted
                }
                else
                {
                    _logger.LogError(message: ex.Message);
                    return View(viewName: "Error", model: ex);
                }
            }
        }

        #endregion
    }
}
