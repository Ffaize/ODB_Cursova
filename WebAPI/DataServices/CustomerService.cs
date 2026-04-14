using WebAPI.Entities;
using WebAPI.Entities.DTOs;
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
            // Generate ID if it's empty
            if (customer.Id == Guid.Empty)
            {
                customer.Id = Guid.NewGuid();
                logger.LogDebug("Generated new ID for customer: {CustomerId}", customer.Id);
            }
            
            // Set CreatedAt if not set
            if (customer.CreatedAt == default)
            {
                customer.CreatedAt = DateTime.UtcNow;
            }
            
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

        public async Task<CreateCustomerWithAccountResponse?> CreateCustomerWithAccount(CreateCustomerWithAccountRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Surname) ||
                    string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    logger.LogWarning("Invalid input for CreateCustomerWithAccount: missing required fields");
                    return null;
                }

                var dynamicParams = new Dictionary<string, object?>
                {
                    { "Name", request.Name.Trim() },
                    { "Surname", request.Surname.Trim() },
                    { "Email", request.Email.Trim() },
                    { "PasswordHash", request.Password }
                };

                var result = await DbAccessService.ExecuteStoredProcedure<CreateCustomerWithAccountResponse>(
                    "sp_Customers_CreateWithAccount", dynamicParams);

                if (result == null)
                {
                    logger.LogError("ExecuteStoredProcedure returned null for sp_Customers_CreateWithAccount");
                }

                return result;
            }
            catch (InvalidOperationException ex)
            {
                // This happens when SP throws RAISERROR
                if (ex.Message.Contains("Email already exists"))
                {
                    logger.LogWarning("CreateCustomerWithAccount failed: Email already exists");
                }
                else
                {
                    logger.LogError(ex, "Error creating customer with account (SQL error). Details: {ErrorMessage}", ex.InnerException?.Message ?? ex.Message);
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating customer with account. Details: {ErrorMessage}", ex.Message);
                return null;
            }
        }

        public async Task<GetCustomerAccountsResponse?> GetCustomerAccountsWithBalance(Guid customerId)
        {
            try
            {
                if (customerId == Guid.Empty)
                {
                    logger.LogWarning("Invalid CustomerId");
                    return null;
                }

                var accounts = await DbAccessService.GetAllByParameter<CustomerAccountItem>(
                    "sp_Customers_GetAllAccountsWithBalance", "CustomerId", customerId);

                return new GetCustomerAccountsResponse
                {
                    CustomerId = customerId,
                    Accounts = accounts
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting customer accounts");
                return null;
            }
        }
    }
}
