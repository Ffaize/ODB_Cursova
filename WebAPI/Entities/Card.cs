using WebAPI.Entities.Enums;

namespace WebAPI.Entities
{
    public class Card
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public CardStatus Status { get; set; }
        public string CardHolderName { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Cvv { get; set; }
        public Guid BillingNumberId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
