using WebAPI.Entities;
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
    }
}
