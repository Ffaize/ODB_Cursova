namespace WebAPI.Entities.ViewResults
{
    public class CardPortfolioStatusResult
    {
        public Guid Id { get; set; }
        public string CardholderName { get; set; }
        public string CardNumberLast4 { get; set; }
        public string CardNumber { get; set; }
        public string CardStatus { get; set; }
        public string CardHolderName { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int DaysUntilExpiration { get; set; }
        public string ExpirationStatus { get; set; }
        public string Cvv { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}
