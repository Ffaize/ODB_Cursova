namespace WebAPI.Entities.DTOs
{
    public class GetCustomerAccountsResponse
    {
        public Guid CustomerId { get; set; }
        public List<CustomerAccountItem> Accounts { get; set; } = new List<CustomerAccountItem>();
    }
}
