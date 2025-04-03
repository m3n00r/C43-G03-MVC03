using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransFerObjects.DepartmentDtos
{
    public class DepartmentDto
    {
        public int DeptId { get; set; }
        public string? Name { get; set; }

        public string code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DateOfcreaction { get; set; }

    }
}
