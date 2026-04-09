using WebAPI.Entities.ViewResults;
using WebAPI.Helpers;

namespace WebAPI.DataServices.Views
{
    public class EmployeeBranchStatsService(ILogger<EmployeeBranchStatsService> logger)
    {
        public async Task<List<EmployeeBranchStatsResult>> GetAll()
        {
            return await DbAccessService.GetItems<EmployeeBranchStatsResult>("sp_EmployeeBranchStats_GetAll");
        }

        public async Task<List<EmployeeBranchStatsResult>> GetByBranchId(Guid branchId)
        {
            return await DbAccessService.GetAllByParameter<EmployeeBranchStatsResult>(
                "sp_EmployeeBranchStats_GetByBranchId", "BranchId", branchId);
        }

        public async Task<List<EmployeeBranchStatsResult>> GetTopPerformers(int topCount = 10)
        {
            return await DbAccessService.GetAllByParameter<EmployeeBranchStatsResult>(
                "sp_EmployeeBranchStats_GetTopPerformers", "TopCount", topCount);
        }
    }
}
