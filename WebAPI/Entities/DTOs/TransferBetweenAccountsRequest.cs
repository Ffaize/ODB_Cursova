namespace WebAPI.Entities.DTOs
{
    public class TransferBetweenAccountsRequest
    {
        public Guid FromBillingNumberId { get; set; }
        public Guid ToBillingNumberId { get; set; }
        public decimal Amount { get; set; }
    }
}
