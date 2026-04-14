namespace WebAPI.Entities.DTOs
{
    public class TransferToOtherCustomerRequest
    {
        public Guid FromBillingNumberId { get; set; }
        public string ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
