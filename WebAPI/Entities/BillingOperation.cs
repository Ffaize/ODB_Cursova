using WebAPI.Entities.Enums;

namespace WebAPI.Entities
{
    public class BillingOperation
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public PaymentPurpose PaymentPurpose { get; set; }
        public Guid? UserId { get; set; }
        public Guid? BillingNumberId { get; set; }
        public Guid? CreditId { get; set; }
    }
}
