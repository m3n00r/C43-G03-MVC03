﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DLL.Models.DepartmentModel;
using Demo.DLL.Models.Shared;
using Demo.DLL.Models.Shared.Enums;

namespace Demo.DLL.Models.EmployeeModel
{
    public class Employee :BaseEntity
    {
        public string Name { get; set; } = null!;
         public int Age { get; set; }
         public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; } 
        public DateTime HireDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        public string? ImageName { get; set; }

    }
}
