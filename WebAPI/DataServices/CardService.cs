using WebAPI.Entities;
using WebAPI.Entities.ExtendedEntities;
using WebAPI.Helpers;

namespace WebAPI.DataServices
{
    public class CardService(ILogger<CardService> logger)
    {
        public async Task<List<Card>> GetAllCards()
        {
            return await DbAccessService.GetItems<Card>("sp_Cards_GetAll");
        }

        public async Task<Card?> GetCardById(Guid id)
        {
            return await DbAccessService.GetItemById<Card>("sp_Cards_GetById", id);
        }

        public async Task<List<ExtendedCard>> GetExtendedAllCards()
        {
            return await DbAccessService.GetItems<ExtendedCard>("sp_Cards_GetExtended");
        }

        public async Task<ExtendedCard?> GetExtendedCardById(Guid id)
        {
            return await DbAccessService.GetItemById<ExtendedCard>("sp_Cards_GetByIdExtended", id);
        }

        public async Task<bool> AddCard(Card card)
        {
            var rowsAffected = await DbAccessService.AddRecord("sp_Cards_Add", card);
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateCard(Card card)
        {
            var rowsAffected = await DbAccessService.UpdateRecord("sp_Cards_Update", card);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteCard(Guid id)
        {
            var rowsAffected = await DbAccessService.DeleteRecord("sp_Cards_Delete", id);
            return rowsAffected > 0;
        }

        public async Task<bool> AddMockData(int count = 1)
        {
            var cards = Faker.GenerateMockCards(count);
            foreach (var card in cards)
            {
                var success = await AddCard(card);
                if (success) continue;
                logger.LogError("Failed to add mock card with ID {CardId}", card.Id);
                return false;
            }
            return true;
        }
    }
}
