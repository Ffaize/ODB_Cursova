using WebAPI.Entities;
using WebAPI.Entities.ExtendedEntities;
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

        public async Task<List<ExtendedBillingNumber>> GetExtendedAllBillingNumbers()
        {
            return await DbAccessService.GetItems<ExtendedBillingNumber>("sp_BillingNumbers_GetExtended");
        }

        public async Task<ExtendedBillingNumber?> GetExtendedBillingNumberById(Guid id)
        {
            return await DbAccessService.GetItemById<ExtendedBillingNumber>("sp_BillingNumbers_GetByIdExtended", id);
        }

        public async Task<bool> AddBillingNumber(BillingNumber billingNumber)
        {
            // Generate ID if it's empty
            if (billingNumber.Id == Guid.Empty)
            {
                billingNumber.Id = Guid.NewGuid();
            }
            
            // Set CreatedAt if not set
            if (billingNumber.CreatedAt == default)
            {
                billingNumber.CreatedAt = DateTime.UtcNow;
            }
            
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

        public async Task<bool> AddMockData(int count = 1, CustomerService? customerService = null)
        {
            try
            {
                // Get existing customers to use their IDs
                var customers = await (customerService?.GetAllCustomers() ?? Task.FromResult(new List<Customer>()));
                
                if (!customers.Any())
                {
                    logger.LogWarning("No customers found. Cannot generate mock billing numbers without valid CustomerId");
                    return false;
                }

                var billingNumbers = Faker.GenerateMockBillingNumbers(count, customers.Select(c => c.Id).ToList());
                foreach (var bn in billingNumbers)
                {
                    var success = await AddBillingNumber(bn);
                    if (success) continue;
                    logger.LogError("Failed to add mock billing number with ID {BillingNumberId}", bn.Id);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error generating mock billing numbers");
                return false;
            }
        }
    }
}
