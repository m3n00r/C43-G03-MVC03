using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransFerObjects.DepartmentDtos
{
    public class CreatedDepartementDto
    {
        [Required(ErrorMessage = "Name Is Required !!!!")]
        public string Name { get; set; } = null!;
        [Required]

        public string Code { get; set; } = null!;
        public DateOnly DateOfCreaction { get; set; }
        public string? Description { get; set; }
    }
}
