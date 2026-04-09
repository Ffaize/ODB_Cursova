using WebAPI.Entities.ViewResults;
using WebAPI.Helpers;

namespace WebAPI.DataServices.Views
{
    public class AuditTrailService(ILogger<AuditTrailService> logger)
    {
        public async Task<List<AuditTrailResult>> GetAll()
        {
            return await DbAccessService.GetItems<AuditTrailResult>("sp_AuditTrail_GetAll");
        }

        public async Task<List<AuditTrailResult>> GetByOperationType(string operationType)
        {
            return await DbAccessService.GetAllByParameter<AuditTrailResult>(
                "sp_AuditTrail_GetByOperationType", "OperationType", operationType);
        }

        public async Task<List<AuditTrailResult>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var results = await DbAccessService.GetItems<AuditTrailResult>("sp_AuditTrail_GetByDateRange");
            return results.Where(r => r.EventDate >= startDate && r.EventDate <= endDate).ToList();
        }
    }
}
