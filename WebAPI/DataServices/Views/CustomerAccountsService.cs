using WebAPI.Entities.ViewResults;
using WebAPI.Helpers;

namespace WebAPI.DataServices.Views
{
    public class CustomerAccountsService(ILogger<CustomerAccountsService> logger)
    {
        public async Task<List<CustomerAccountsSummaryResult>> GetAll()
        {
            return await DbAccessService.GetItems<CustomerAccountsSummaryResult>("sp_CustomerAccounts_GetAll");
        }

        public async Task<CustomerAccountsSummaryResult?> GetByCustomerId(Guid customerId)
        {
            return await DbAccessService.GetOneByParameter<CustomerAccountsSummaryResult>(
                "sp_CustomerAccounts_GetByCustomerId", "CustomerId", customerId);
        }

        public async Task<List<CustomerAccountsSummaryResult>> GetHighBalance(decimal minimumBalance = 10000)
        {
            return await DbAccessService.GetAllByParameter<CustomerAccountsSummaryResult>(
                "sp_CustomerAccounts_GetHighBalance", "MinimumBalance", minimumBalance);
        }
    }
}
