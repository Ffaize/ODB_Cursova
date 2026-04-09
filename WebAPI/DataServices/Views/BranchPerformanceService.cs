using WebAPI.Entities.ViewResults;
using WebAPI.Helpers;

namespace WebAPI.DataServices.Views
{
    public class BranchPerformanceService(ILogger<BranchPerformanceService> logger)
    {
        public async Task<List<BranchPerformanceResult>> GetAll()
        {
            return await DbAccessService.GetItems<BranchPerformanceResult>("sp_BranchPerformance_GetAll");
        }

        public async Task<BranchPerformanceResult?> GetByBranchId(Guid branchId)
        {
            return await DbAccessService.GetOneByParameter<BranchPerformanceResult>(
                "sp_BranchPerformance_GetByBranchId", "BranchId", branchId);
        }

        public async Task<List<BranchPerformanceResult>> GetTopPerformers(int topCount = 10)
        {
            return await DbAccessService.GetAllByParameter<BranchPerformanceResult>(
                "sp_BranchPerformance_GetTopPerformers", "TopCount", topCount);
        }
    }
}
