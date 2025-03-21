using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DataTransFerObjects;
using Demo.BLL.Factories;
using Demo.DLL.Models;
using Demo.DLL.Repositories;

namespace Demo.BLL.Services
{
    class DepartmentService(IDepartmentRepository _departmentRepository)
    {
        //Get All Departments

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();

            //var departmentsToReturn = departments.Select(D => new DepartmentDto()
            //{
            //    DeptId = D.Id,
            //    code = D.code,
            //    Description=D.Description ,
            //    Name = D.Name,
            //    //DateOfcreaction = DateOnly.FromDateTime(D.CreatedOn)
            //});
            return departments.Select(D => D.ToDepartmentDto());
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);

            return department is null ? null : department.ToDepartmentDetailsDto();


        }

        public int AddDepartment(CreatedDepartementDto departementDto)
        {
            //_departmentRepository.Add(departementDto)
            var departement= departementDto.ToEntity();
            return _departmentRepository.Add(departement);

        }

        public int UpdateDepartment(UpdatedDepartementDto departementDto) 
        {
       return _departmentRepository.Update(departementDto.ToEntity());
        
        }

        public bool DeleteDepartment(int id)
        {
            {
                var Dept = _departmentRepository.GetById(id);
                if (Dept is null) return false;
                else
                {
                    int Result = _departmentRepository.Remove(Dept);
                    if (Result > 0) return true;
                    else return false;
                }
            }

        }
    }
}
