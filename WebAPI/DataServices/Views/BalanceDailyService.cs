using WebAPI.Entities.ViewResults;
using WebAPI.Helpers;

namespace WebAPI.DataServices.Views
{
    public class BalanceDailyService(ILogger<BalanceDailyService> logger)
    {
        public async Task<List<BalanceDailyResult>> GetAll()
        {
            return await DbAccessService.GetItems<BalanceDailyResult>("sp_BalanceDaily_GetAll");
        }

        public async Task<List<BalanceDailyResult>> GetByCustomerId(Guid customerId)
        {
            return await DbAccessService.GetAllByParameter<BalanceDailyResult>(
                "sp_BalanceDaily_GetByCustomerId", "CustomerId", customerId);
        }

        public async Task<List<BalanceDailyResult>> GetLowBalance(decimal thresholdBalance = 1000)
        {
            return await DbAccessService.GetAllByParameter<BalanceDailyResult>(
                "sp_BalanceDaily_GetLowBalance", "ThresholdBalance", thresholdBalance);
        }
    }
}
