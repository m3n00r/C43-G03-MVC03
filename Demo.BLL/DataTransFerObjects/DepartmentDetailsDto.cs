using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DLL.Models;

namespace Demo.BLL.DataTransFerObjects
{
    public class DepartmentDetailsDto
    {
      
        public int Id { get; set; }//pk
        public int CreatedBy { get; set; }//user id
        public DateOnly CreatedOn { get; set; }

        public int LastModifedBy { get; set; }
        public DateOnly LastModifedOn { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public string? Description { get; set; }
      
    }
}
