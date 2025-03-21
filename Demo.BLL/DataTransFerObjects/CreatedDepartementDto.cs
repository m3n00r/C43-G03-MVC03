using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransFerObjects
{
     class CreatedDepartementDto
    { 
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateOnly DateOfCreaction { get; set; }
        public string? Description { get; set; }
    }
}
