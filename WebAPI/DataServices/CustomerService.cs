using WebAPI.Entities;
using WebAPI.Helpers;

namespace WebAPI.DataServices
{
    public class CustomerService(ILogger<CustomerService> logger)
    {
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await DbAccessService.GetItems<Customer>("sp_Customers_GetAll");
        }

        public async Task<Customer?> GetCustomerById(Guid id)
        {
            return await DbAccessService.GetItemById<Customer>("sp_Customers_GetById", id);
        }

        public async Task<bool> AddCustomer(Customer customer)
        {
            var rowsAffected = await DbAccessService.AddRecord("sp_Customers_Add", customer);
            return rowsAffected > 0;
        }

        public async Task<bool> AddMockCustomers(int count = 1)
        {
            var customers = Faker.GenerateMockCustomers(count);
            foreach (var customer in customers)
            {
                var success = await AddCustomer(customer);
                if (success) continue;
                logger.LogError("Failed to add mock customer with ID {CustomerId}", customer.Id);
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            var rowsAffected = await DbAccessService.UpdateRecord("sp_Customers_Update", customer);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteCustomer(Guid id)
        {
            var rowsAffected = await DbAccessService.DeleteRecord("sp_Customers_Delete", id);
            return rowsAffected > 0;
        }
    }
}
