using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DLL.Data.Contexts;
using Demo.DLL.Models.EmployeeModel;
using Demo.DLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Demo.DLL.Repositories.Classes
{
  public class EmployeeRepository (ApplicationDbContext dbContext): GenericRepository<Employee>(dbContext), IEmployeeRepository 
    {
       
    }
}
