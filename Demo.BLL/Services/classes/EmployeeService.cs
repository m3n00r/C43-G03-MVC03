using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BLL.DataTransFerObjects.EmployeeDtos;
using Demo.BLL.Services.Interfaces;
using Demo.DLL.Models.EmployeeModel;
using Demo.DLL.Repositories.Interfaces;

namespace Demo.BLL.Services.classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository,IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking =false)
        {
            var Employees = _employeeRepository.GetAll(withTracking);

            var EmployeesDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(Employees);

            return EmployeesDto;
           

        }

        public EmployeeDetailsDto? GetEmployeebyId(int id)
        {
            var employee = _employeeRepository.GetById(id);
           return employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(employee); 
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee =_mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            return _employeeRepository.Add(employee);
        }

        public int UpdatedEmployee(UpdatedEmployeeDto employeeDto)
        {
            return _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
        }

        public bool DeleteEmployee(int id)
        {
           var employee =_employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) > 0 ? true:false;
            }

        }

      
      
    }
}
