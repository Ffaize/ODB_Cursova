namespace WebAPI.Entities.DTOs
{
    public class OpenCreditResponse
    {
        public Guid CreditId { get; set; }
        public Guid BillingNumberId { get; set; }
        public string AccountNumber { get; set; }
        public decimal MonthlyPayment { get; set; }
        public DateTime FirstPaymentDate { get; set; }
        public decimal FullAmount { get; set; }
    }
}
