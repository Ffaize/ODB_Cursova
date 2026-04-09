using WebAPI.Entities.ViewResults;
using WebAPI.Helpers;

namespace WebAPI.DataServices.Views
{
    public class CardPortfolioService(ILogger<CardPortfolioService> logger)
    {
        public async Task<List<CardPortfolioStatusResult>> GetAll()
        {
            return await DbAccessService.GetItems<CardPortfolioStatusResult>("sp_CardPortfolio_GetAll");
        }

        public async Task<List<CardPortfolioStatusResult>> GetByStatus(string status)
        {
            return await DbAccessService.GetAllByParameter<CardPortfolioStatusResult>(
                "sp_CardPortfolio_GetByStatus", "Status", status);
        }

        public async Task<List<CardPortfolioStatusResult>> GetExpiringSoon(int daysUntilExpiry = 90)
        {
            return await DbAccessService.GetAllByParameter<CardPortfolioStatusResult>(
                "sp_CardPortfolio_GetExpiringSoon", "DaysUntilExpiry", daysUntilExpiry);
        }
    }
}
