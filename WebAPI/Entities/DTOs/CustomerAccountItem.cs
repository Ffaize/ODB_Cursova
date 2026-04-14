namespace WebAPI.Entities.DTOs
{
    public class CustomerAccountItem
    {
        public Guid BillingNumberId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public int Status { get; set; }
        public int AccountType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
