using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DLL.Data.Contexts;
using Demo.DLL.Models.DepartmentModel;
using Demo.DLL.Repositories.Interfaces;

namespace Demo.DLL.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContext dbContext) : GenericRepository<Department>(dbContext),IDepartmentRepository
    {
       

     

    }
}
