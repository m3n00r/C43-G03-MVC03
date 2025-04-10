using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DataTransFerObjects.DepartmentDtos;
using Demo.DLL.Models.DepartmentModel;

namespace Demo.BLL.Factories
{
    static class DepartementFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department D)
        {
            
            return new DepartmentDto()
            {
                DeptId = D.Id,
                code = D.code,
                Description = D.Description ?? "",
                Name = D.Name,
                //DateOfcreaction = DateOnly.FromDateTime(D.CreatedOn)

            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                    Name= department.Name,
                //CreatedOn=DateOnly.FromDateTime(department.CreatedOn);

            };
        }

        public static Department ToEntity(this CreatedDepartementDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())

            };
        }

        public static Department ToEntity(this UpdatedDepartementDto departementDto)
        {
            return new Department()
            {
                Id= departementDto.Id,
                Name = departementDto.Name,
                code= departementDto.Code,
                CreatedOn =departementDto.DateOfCreaction.ToDateTime(new TimeOnly()),
                Description = departementDto.Description 

            };
        }

    }
}
