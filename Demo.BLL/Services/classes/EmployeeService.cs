using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DataTransFerObjects.EmployeeDtos;
using Demo.BLL.Services.Interfaces;
using Demo.DLL.Repositories.Interfaces;

namespace Demo.BLL.Services.classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking =false)
        {
            var Employees=_employeeRepository.GetAll(withTracking);
            var employeesDto = Employees.Select(Emp => new EmployeeDto
            {
                Id = Emp.Id,
                Name = Emp.Name,
                Age = Emp.Age,
                Email = Emp.Email,
                IsActive = Emp.IsActive,
                Salary = Emp.Salary,
                EmployeeType = Emp.EmployeeType.ToString(),
                Gender = Emp.Gender.ToString()
            });

            return employeesDto;

        }

        public EmployeeDetailsDto? GetEmployeebyId(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return null;
            else return new EmployeeDetailsDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                HiringDate=DateOnly.FromDateTime(employee.HireDate),   
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                EmployeeType = employee.EmployeeType.ToString(),
                Gender = employee.Gender.ToString(),
                CreatedBy = 1,
                //CreatedOn = employee.CreatedOn,
                LastModifiedBy = 1,
              
                //LastModifiedOn = employee.LastModifedOn
            };
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

      
        public int UpdatedEmployee(UpdatedEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }
    }
}
