using WebAPI.Entities;
using WebAPI.Entities.DTOs;
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
            // Generate ID if it's empty
            if (card.Id == Guid.Empty)
            {
                card.Id = Guid.NewGuid();
            }
            
            // Set CreatedAt if not set
            if (card.CreatedAt == default)
            {
                card.CreatedAt = DateTime.UtcNow;
            }
            
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

        public async Task<bool> AddMockData(int count = 1, BillingNumberService? billingNumberService = null, CustomerService? customerService = null)
        {
            try
            {
                // Get existing billing numbers
                var billingNumbers = await (billingNumberService?.GetAllBillingNumbers() ?? Task.FromResult(new List<BillingNumber>()));
                if (!billingNumbers.Any())
                {
                    logger.LogWarning("No billing numbers found. Cannot generate mock cards without valid BillingNumberId");
                    return false;
                }

                // Get existing customers
                var customers = await (customerService?.GetAllCustomers() ?? Task.FromResult(new List<Customer>()));
                if (!customers.Any())
                {
                    logger.LogWarning("No customers found. Cannot generate mock cards without valid CustomerId");
                    return false;
                }

                var billingNumberIds = billingNumbers.Select(b => b.Id).ToList();
                var customerIds = customers.Select(c => c.Id).ToList();

                var cards = Faker.GenerateMockCards(count, billingNumberIds, customerIds);
                foreach (var card in cards)
                {
                    var success = await AddCard(card);
                    if (success) continue;
                    logger.LogError("Failed to add mock card with ID {CardId}", card.Id);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error generating mock cards");
                return false;
            }
        }

        public async Task<IssueCardResponse?> IssueCard(IssueCardRequest request)
        {
            try
            {
                if (request == null || request.BillingNumberId == Guid.Empty)
                {
                    logger.LogWarning("Invalid input for IssueCard");
                    return null;
                }

                var dynamicParams = new Dictionary<string, object?>
                {
                    { "BillingNumberId", request.BillingNumberId },
                    { "CardType", request.CardType }
                };

                var result = await DbAccessService.ExecuteStoredProcedure<IssueCardResponse>(
                    "sp_Cards_IssueCard", dynamicParams);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error issuing card");
                return null;
            }
        }

        public async Task<bool> BlockCard(Guid cardId)
        {
            try
            {
                if (cardId == Guid.Empty)
                {
                    logger.LogWarning("Invalid CardId");
                    return false;
                }

                var result = await DbAccessService.GetOneByParameter<int>(
                    "sp_Cards_BlockCard", "CardId", cardId);

                return result > 0;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error blocking card");
                return false;
            }
        }
    }
}
