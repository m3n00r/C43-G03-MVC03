using Demo.BLL.DataTransFerObjects.DepartmentDtos;

namespace Demo.BLL.Services.Interfaces
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