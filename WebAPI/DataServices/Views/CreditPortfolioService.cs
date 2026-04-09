using WebAPI.Entities.ViewResults;
using WebAPI.Helpers;

namespace WebAPI.DataServices.Views
{
    public class CreditPortfolioService(ILogger<CreditPortfolioService> logger)
    {
        public async Task<List<CreditPortfolioResult>> GetAll()
        {
            return await DbAccessService.GetItems<CreditPortfolioResult>("sp_CreditPortfolio_GetAll");
        }

        public async Task<List<CreditPortfolioResult>> GetByCreditStatus(string status)
        {
            return await DbAccessService.GetAllByParameter<CreditPortfolioResult>(
                "sp_CreditPortfolio_GetByCreditStatus", "Status", status);
        }

        public async Task<List<CreditPortfolioResult>> GetOverdue()
        {
            return await DbAccessService.GetItems<CreditPortfolioResult>("sp_CreditPortfolio_GetOverdue");
        }

        public async Task<List<CreditPortfolioResult>> GetByCurrency(string currency)
        {
            return await DbAccessService.GetAllByParameter<CreditPortfolioResult>(
                "sp_CreditPortfolio_GetByCurrency", "Currency", currency);
        }
    }
}
