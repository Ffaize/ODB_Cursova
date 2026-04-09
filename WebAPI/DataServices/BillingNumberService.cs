using WebAPI.Entities;
using WebAPI.Helpers;

namespace WebAPI.DataServices
{
    public class BillingNumberService(ILogger<BillingNumberService> logger)
    {
        public async Task<List<BillingNumber>> GetAllBillingNumbers()
        {
            return await DbAccessService.GetItems<BillingNumber>("sp_BillingNumbers_GetAll");
        }

        public async Task<BillingNumber?> GetBillingNumberById(Guid id)
        {
            return await DbAccessService.GetItemById<BillingNumber>("sp_BillingNumbers_GetById", id);
        }

        public async Task<bool> AddBillingNumber(BillingNumber billingNumber)
        {
            var rowsAffected = await DbAccessService.AddRecord("sp_BillingNumbers_Add", billingNumber);
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateBillingNumber(BillingNumber billingNumber)
        {
            var rowsAffected = await DbAccessService.UpdateRecord("sp_BillingNumbers_Update", billingNumber);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteBillingNumber(Guid id)
        {
            var rowsAffected = await DbAccessService.DeleteRecord("sp_BillingNumbers_Delete", id);
            return rowsAffected > 0;
        }

        public async Task<bool> AddMockData(int count = 1)
        {
            var billingNumbers = Faker.GenerateMockBillingNumbers(count);
            foreach (var billingNumber in billingNumbers)
            {
                var success = await AddBillingNumber(billingNumber);
                if (success) continue;
                logger.LogError("Failed to add mock billing number with ID {BillingNumberId}", billingNumber.Id);
                return false;
            }
            return true;
        }
    }
}
