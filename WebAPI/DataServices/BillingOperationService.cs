using WebAPI.Entities;
using WebAPI.Entities.DTOs;
using WebAPI.Entities.ExtendedEntities;
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

        public async Task<List<ExtendedBillingOperation>> GetExtendedAllBillingOperations()
        {
            return await DbAccessService.GetItems<ExtendedBillingOperation>("sp_BillingOperations_GetExtended");
        }

        public async Task<ExtendedBillingOperation?> GetExtendedBillingOperationById(Guid id)
        {
            return await DbAccessService.GetItemById<ExtendedBillingOperation>("sp_BillingOperations_GetByIdExtended", id);
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

        public async Task<CheckBalanceResponse?> CheckBalance(Guid billingNumberId)
        {
            try
            {
                if (billingNumberId == Guid.Empty)
                {
                    logger.LogWarning("Invalid BillingNumberId");
                    return null;
                }

                var result = await DbAccessService.GetOneByParameter<CheckBalanceResponse>(
                    "sp_BillingNumbers_GetBalance", "BillingNumberId", billingNumberId);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking balance");
                return null;
            }
        }

        public async Task<TransferResponse?> TransferBetweenAccounts(TransferBetweenAccountsRequest request)
        {
            try
            {
                if (request == null || request.FromBillingNumberId == Guid.Empty || request.ToBillingNumberId == Guid.Empty)
                {
                    logger.LogWarning("Invalid input for TransferBetweenAccounts");
                    return null;
                }

                if (request.Amount <= 0)
                {
                    logger.LogWarning("Invalid amount for transfer");
                    return null;
                }

                var dynamicParams = new Dictionary<string, object?>
                {
                    { "FromBillingNumberId", request.FromBillingNumberId },
                    { "ToBillingNumberId", request.ToBillingNumberId },
                    { "Amount", request.Amount }
                };

                var result = await DbAccessService.ExecuteStoredProcedure<TransferResponse>(
                    "sp_BillingOperations_TransferBetweenAccounts", dynamicParams);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error transferring between accounts");
                return null;
            }
        }

        public async Task<TransferResponse?> TransferToOtherCustomer(TransferToOtherCustomerRequest request)
        {
            try
            {
                if (request == null || request.FromBillingNumberId == Guid.Empty)
                {
                    logger.LogWarning("Invalid input for TransferToOtherCustomer");
                    return null;
                }

                if (string.IsNullOrWhiteSpace(request.ToAccountNumber) || string.IsNullOrWhiteSpace(request.Description))
                {
                    logger.LogWarning("Invalid input - account number and description required");
                    return null;
                }

                if (request.Amount <= 0)
                {
                    logger.LogWarning("Invalid amount for transfer");
                    return null;
                }

                var dynamicParams = new Dictionary<string, object?>
                {
                    { "FromBillingNumberId", request.FromBillingNumberId },
                    { "ToAccountNumber", request.ToAccountNumber.Trim() },
                    { "Amount", request.Amount },
                    { "Description", request.Description.Trim() }
                };

                var result = await DbAccessService.ExecuteStoredProcedure<TransferResponse>(
                    "sp_BillingOperations_TransferToOtherCustomer", dynamicParams);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error transferring to other customer");
                return null;
            }
        }

        public async Task<PayBillingResponse?> PayBilling(PayBillingRequest request)
        {
            try
            {
                if (request == null || request.BillingNumberId == Guid.Empty)
                {
                    logger.LogWarning("Invalid input for PayBilling");
                    return null;
                }

                if (request.Amount <= 0)
                {
                    logger.LogWarning("Invalid amount for payment");
                    return null;
                }

                var dynamicParams = new Dictionary<string, object?>
                {
                    { "BillingNumberId", request.BillingNumberId },
                    { "Amount", request.Amount },
                    { "OperationType", request.OperationType }
                };

                var result = await DbAccessService.ExecuteStoredProcedure<PayBillingResponse>(
                    "sp_BillingOperations_PayBilling", dynamicParams);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error paying billing");
                return null;
            }
        }
    }
}
