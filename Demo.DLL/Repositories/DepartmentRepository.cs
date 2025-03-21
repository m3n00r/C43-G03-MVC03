using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DLL.Data.Contexts;

namespace Demo.DLL.Repositories
{
    public class DepartmentRepository(ApplicationDbContext dbContext) : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        //GetAll
        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Departments.ToList();
            else
                return _dbContext.Departments.AsNoTracking().ToList();

        }
        //GetById
        public Department? GetById(int id) => _dbContext.Departments.Find(id);

        //update
        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }

        //Delete

        public int Remove(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        //Insert
        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

    }
}
