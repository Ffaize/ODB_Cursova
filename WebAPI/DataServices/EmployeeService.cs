using WebAPI.Entities;
using WebAPI.Entities.ExtendedEntities;
using WebAPI.Helpers;

namespace WebAPI.DataServices
{
    public class EmployeeService(ILogger<EmployeeService> logger)
    {
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await DbAccessService.GetItems<Employee>("sp_Employees_GetAll");
        }

        public async Task<Employee?> GetEmployeeById(Guid id)
        {
            return await DbAccessService.GetItemById<Employee>("sp_Employees_GetById", id);
        }

        public async Task<List<ExtendedEmployee>> GetExtendedAllEmployees()
        {
            return await DbAccessService.GetItems<ExtendedEmployee>("sp_Employees_GetExtended");
        }

        public async Task<ExtendedEmployee?> GetExtendedEmployeeById(Guid id)
        {
            return await DbAccessService.GetItemById<ExtendedEmployee>("sp_Employees_GetByIdExtended", id);
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            var rowsAffected = await DbAccessService.AddRecord("sp_Employees_Add", employee);
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var rowsAffected = await DbAccessService.UpdateRecord("sp_Employees_Update", employee);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteEmployee(Guid id)
        {
            var rowsAffected = await DbAccessService.DeleteRecord("sp_Employees_Delete", id);
            return rowsAffected > 0;
        }

        public async Task<bool> AddMockData(int count = 1)
        {
            var employees = Faker.GenerateMockEmployees(count);
            foreach (var employee in employees)
            {
                var success = await AddEmployee(employee);
                if (success) continue;
                logger.LogError("Failed to add mock employee with ID {EmployeeId}", employee.Id);
                return false;
            }
            return true;
        }
    }
}
