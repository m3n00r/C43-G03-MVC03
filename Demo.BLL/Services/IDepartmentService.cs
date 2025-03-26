using Demo.BLL.DataTransFerObjects;

namespace Demo.BLL.Services
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartementDto departementDto);
        int CreateDepartment(CreatedDepartementDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdatedDepartementDto departementDto);
    }
}