using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DLL.Models
{
    public class Department :BaseEntity
    {
        public string Name { get; set; } = null!;
        public string code { get; set; } = null!;
        public string? Description { get; set; }
    }
}
