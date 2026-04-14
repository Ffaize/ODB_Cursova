namespace WebAPI.Entities.DTOs
{
    public class PayBillingRequest
    {
        public Guid BillingNumberId { get; set; }
        public decimal Amount { get; set; }
        public int OperationType { get; set; }
    }
}
