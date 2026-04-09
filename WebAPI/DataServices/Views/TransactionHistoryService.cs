using WebAPI.Entities.ViewResults;
using WebAPI.Helpers;

namespace WebAPI.DataServices.Views
{
    public class TransactionHistoryService(ILogger<TransactionHistoryService> logger)
    {
        public async Task<List<TransactionHistoryResult>> GetAll()
        {
            return await DbAccessService.GetItems<TransactionHistoryResult>("sp_TransactionHistory_GetAll");
        }

        public async Task<List<TransactionHistoryResult>> GetByCustomerId(Guid customerId)
        {
            return await DbAccessService.GetAllByParameter<TransactionHistoryResult>(
                "sp_TransactionHistory_GetByCustomerId", "CustomerId", customerId);
        }

        public async Task<List<TransactionHistoryResult>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var results = await DbAccessService.GetItems<TransactionHistoryResult>("sp_TransactionHistory_GetByDateRange");
            return results.Where(r => r.TransactionDate >= startDate && r.TransactionDate <= endDate).ToList();
        }

        public async Task<List<TransactionHistoryResult>> GetLargeTransactions(decimal minimumAmount)
        {
            return await DbAccessService.GetAllByParameter<TransactionHistoryResult>(
                "sp_TransactionHistory_GetLargeTransactions", "MinimumAmount", minimumAmount);
        }
    }
}
