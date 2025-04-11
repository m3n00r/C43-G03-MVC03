using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DataTransFerObjects.DepartmentDtos;
using Demo.BLL.Factories;
using Demo.BLL.Services.Interfaces;
using Demo.DLL.Models;
using Demo.DLL.Repositories.Interfaces;

namespace Demo.BLL.Services.classes
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        //Get All Departments

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

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
            var department = _unitOfWork.DepartmentRepository.GetById(id);


            return department is null ? null : department.ToDepartmentDetailsDto();


        }

        public int AddDepartment(CreatedDepartementDto departementDto)
        {
            //_departmentRepository.Add(departementDto)
            var departement = departementDto.ToEntity();
             _unitOfWork.DepartmentRepository.Add(departement);
            return

               _unitOfWork.SaveChanges();

        }

        public int UpdateDepartment(UpdatedDepartementDto departementDto)
        {
            return _unitOfWork.DepartmentRepository.Update(departementDto.ToEntity());

        }

        public bool DeleteDepartment(int id)
        {
            {
                var Dept = _unitOfWork.DepartmentRepository.GetById(id);
                if (Dept is null) return false;
                else
                {
                    _unitOfWork.DepartmentRepository.Remove(Dept);
                    int Result = _unitOfWork.SaveChanges();
                    if (Result > 0) return true;
                    else return false;
                }
            }

        }

        public int CreateDepartment(CreatedDepartementDto departmentDto)
        {
            throw new NotImplementedException();
        }
    }
}
