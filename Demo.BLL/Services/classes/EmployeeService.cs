using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BLL.DataTransFerObjects.EmployeeDtos;
using Demo.BLL.Services.AttachementServices;
using Demo.BLL.Services.Interfaces;
using Demo.DLL.Models.EmployeeModel;
using Demo.DLL.Repositories.Interfaces;

namespace Demo.BLL.Services.classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork,IMapper _mapper ,
        IAttachementServices _attachementServices) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
        
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));

            var EmployeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

            return EmployeesDto;
        }

        public EmployeeDetailsDto? GetEmployeebyId(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
           return employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(employee); 
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee =_mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);

            if (employeeDto.Image is not null)  
            {

             _attachementServices.Upload(employeeDto.Image,"Images");

            }






            _unitOfWork.EmployeeRepository.Add(employee);
             return
                
                _unitOfWork.SaveChanges();
        }

        public int UpdatedEmployee(UpdatedEmployeeDto employeeDto)
        {
            return _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
        }

        public bool DeleteEmployee(int id)
        {
           var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _unitOfWork.EmployeeRepository.Update(employee) > 0 ? true:false;
            }

        }

        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking = false)
        {
            throw new NotImplementedException();
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }
    }
}
