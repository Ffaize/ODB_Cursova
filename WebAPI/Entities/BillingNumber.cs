namespace WebAPI.Entities
{
    public class BillingNumber
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public AccountType AccountType { get; set; }
        public AccountStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
    }

    public enum AccountType
    {
        Checking = 1,
        Savings = 2,
        MoneyMarket = 3,
        CertificateOfDeposit = 4,
        BusinessAccount = 5
    }

    public enum AccountStatus
    {
        Active = 1,
        Inactive = 2,
        Frozen = 3,
        Closed = 4,
        Suspended = 5
    }
}
