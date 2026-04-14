using WebAPI.Entities;
using WebAPI.Entities.DTOs;
using WebAPI.Entities.ExtendedEntities;
using WebAPI.Helpers;

namespace WebAPI.DataServices
{
    public class CreditService(ILogger<CreditService> logger)
    {
        public async Task<List<Credit>> GetAllCredits()
        {
            return await DbAccessService.GetItems<Credit>("sp_Credits_GetAll");
        }

        public async Task<Credit?> GetCreditById(Guid id)
        {
            return await DbAccessService.GetItemById<Credit>("sp_Credits_GetById", id);
        }

        public async Task<List<ExtendedCredit>> GetExtendedAllCredits()
        {
            return await DbAccessService.GetItems<ExtendedCredit>("sp_Credits_GetExtended");
        }

        public async Task<ExtendedCredit?> GetExtendedCreditById(Guid id)
        {
            return await DbAccessService.GetItemById<ExtendedCredit>("sp_Credits_GetByIdExtended", id);
        }

        public async Task<bool> AddCredit(Credit credit)
        {
            var rowsAffected = await DbAccessService.AddRecord("sp_Credits_Add", credit);
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateCredit(Credit credit)
        {
            var rowsAffected = await DbAccessService.UpdateRecord("sp_Credits_Update", credit);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteCredit(Guid id)
        {
            var rowsAffected = await DbAccessService.DeleteRecord("sp_Credits_Delete", id);
            return rowsAffected > 0;
        }

        public async Task<bool> AddMockData(int count = 1)
        {
            var credits = Faker.GenerateMockCredits(count);
            foreach (var credit in credits)
            {
                var success = await AddCredit(credit);
                if (success) continue;
                logger.LogError("Failed to add mock credit with ID {CreditId}", credit.Id);
                return false;
            }
            return true;
        }

        public async Task<OpenCreditResponse?> OpenCredit(OpenCreditRequest request)
        {
            try
            {
                if (request == null || request.CustomerId == Guid.Empty)
                {
                    logger.LogWarning("Invalid input for OpenCredit: missing or invalid CustomerId");
                    return null;
                }

                if (request.FullAmount <= 0)
                {
                    logger.LogWarning("Invalid input for OpenCredit: amount must be greater than 0");
                    return null;
                }

                if (request.DurationInMonths <= 0)
                {
                    logger.LogWarning("Invalid input for OpenCredit: duration must be greater than 0");
                    return null;
                }

                var dynamicParams = new Dictionary<string, object?>
                {
                    { "CustomerId", request.CustomerId },
                    { "FullAmount", request.FullAmount },
                    { "DurationInMonths", request.DurationInMonths },
                    { "Currency", request.Currency ?? "UAH" }
                };

                var result = await DbAccessService.ExecuteStoredProcedure<OpenCreditResponse>(
                    "sp_Credits_OpenCredit", dynamicParams);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error opening credit");
                return null;
            }
        }

        public async Task<List<PaymentScheduleItem>?> GetPaymentSchedule(Guid creditId)
        {
            try
            {
                if (creditId == Guid.Empty)
                {
                    logger.LogWarning("Invalid CreditId");
                    return null;
                }

                var schedule = await DbAccessService.GetAllByParameter<PaymentScheduleItem>(
                    "sp_Credits_GetPaymentSchedule", "CreditId", creditId);

                return schedule;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting payment schedule");
                return null;
            }
        }

        public async Task<bool> CloseCredit(Guid creditId)
        {
            try
            {
                if (creditId == Guid.Empty)
                {
                    logger.LogWarning("Invalid CreditId");
                    return false;
                }

                var rowsAffected = await DbAccessService.GetOneByParameter<int>(
                    "sp_Credits_CloseCredit", "CreditId", creditId);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error closing credit");
                return false;
            }
        }
    }
}
