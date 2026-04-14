namespace WebAPI.Entities.DTOs
{
    public class CreateCustomerWithAccountResponse
    {
        public Guid CustomerId { get; set; }
        public Guid BillingNumberId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}
