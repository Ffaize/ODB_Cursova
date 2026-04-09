namespace WebAPI.Entities.ViewResults
{
    public class BalanceDailyResult
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid AccountId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int OperationsLast24H { get; set; }
        public int OperationsLast7D { get; set; }
        public decimal OutflowLast24H { get; set; }
        public decimal InflowLast24H { get; set; }
        public DateTime AccountCreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int AccountAgeDays { get; set; }
    }
}
