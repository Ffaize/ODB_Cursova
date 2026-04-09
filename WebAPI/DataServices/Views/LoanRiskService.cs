using WebAPI.Entities.ViewResults;
using WebAPI.Helpers;

namespace WebAPI.DataServices.Views
{
    public class LoanRiskService(ILogger<LoanRiskService> logger)
    {
        public async Task<List<CustomerLoanRiskResult>> GetAll()
        {
            return await DbAccessService.GetItems<CustomerLoanRiskResult>("sp_LoanRisk_GetAll");
        }

        public async Task<List<CustomerLoanRiskResult>> GetByRiskLevel(string riskLevel)
        {
            return await DbAccessService.GetAllByParameter<CustomerLoanRiskResult>(
                "sp_LoanRisk_GetByRiskLevel", "RiskLevel", riskLevel);
        }

        public async Task<List<CustomerLoanRiskResult>> GetHighRisk()
        {
            return await DbAccessService.GetItems<CustomerLoanRiskResult>("sp_LoanRisk_GetHighRisk");
        }
    }
}
