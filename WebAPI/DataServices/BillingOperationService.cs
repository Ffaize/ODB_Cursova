using WebAPI.Entities;
using WebAPI.Helpers;

namespace WebAPI.DataServices
{
    public class BillingOperationService(ILogger<BillingOperationService> logger)
    {
        public async Task<List<BillingOperation>> GetAllBillingOperations()
        {
            return await DbAccessService.GetItems<BillingOperation>("sp_BillingOperations_GetAll");
        }

        public async Task<BillingOperation?> GetBillingOperationById(Guid id)
        {
            return await DbAccessService.GetItemById<BillingOperation>("sp_BillingOperations_GetById", id);
        }

        public async Task<bool> AddBillingOperation(BillingOperation billingOperation)
        {
            var rowsAffected = await DbAccessService.AddRecord("sp_BillingOperations_Add", billingOperation);
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateBillingOperation(BillingOperation billingOperation)
        {
            var rowsAffected = await DbAccessService.UpdateRecord("sp_BillingOperations_Update", billingOperation);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteBillingOperation(Guid id)
        {
            var rowsAffected = await DbAccessService.DeleteRecord("sp_BillingOperations_Delete", id);
            return rowsAffected > 0;
        }

        public async Task<bool> AddMockData(int count = 1)
        {
            var billingOperations = Faker.GenerateMockBillingOperations(count);
            foreach (var billingOperation in billingOperations)
            {
                var success = await AddBillingOperation(billingOperation);
                if (success) continue;
                logger.LogError("Failed to add mock billing operation with ID {BillingOperationId}", billingOperation.Id);
                return false;
            }
            return true;
        }
    }
}
