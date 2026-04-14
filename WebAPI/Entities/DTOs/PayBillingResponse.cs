namespace WebAPI.Entities.DTOs
{
    public class PayBillingResponse
    {
        public Guid OperationId { get; set; }
        public decimal NewBalance { get; set; }
        public decimal? RemainingToPay { get; set; }
    }
}
