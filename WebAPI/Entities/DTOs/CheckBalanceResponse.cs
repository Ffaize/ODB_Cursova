namespace WebAPI.Entities.DTOs
{
    public class CheckBalanceResponse
    {
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string AccountNumber { get; set; }
        public int Status { get; set; }
    }
}
