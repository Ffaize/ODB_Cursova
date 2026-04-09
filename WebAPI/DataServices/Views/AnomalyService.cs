using WebAPI.Entities.ViewResults;
using WebAPI.Helpers;

namespace WebAPI.DataServices.Views
{
    public class AnomalyService(ILogger<AnomalyService> logger)
    {
        public async Task<List<AnomalyResult>> GetAll()
        {
            return await DbAccessService.GetItems<AnomalyResult>("sp_Anomalies_GetAll");
        }

        public async Task<List<AnomalyResult>> GetRecent(int hoursBack = 24)
        {
            return await DbAccessService.GetAllByParameter<AnomalyResult>(
                "sp_Anomalies_GetRecent", "HoursBack", hoursBack);
        }

        public async Task<List<AnomalyResult>> GetBySeverity(string severity)
        {
            return await DbAccessService.GetAllByParameter<AnomalyResult>(
                "sp_Anomalies_BySeverity", "Severity", severity);
        }
    }
}
