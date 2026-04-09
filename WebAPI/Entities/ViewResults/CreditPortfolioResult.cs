namespace WebAPI.Entities.ViewResults
{
    public class CreditPortfolioResult
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public decimal FullAmount { get; set; }
        public decimal RemainingToPay { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal PercentagePaid { get; set; }
        public decimal MonthlyPayment { get; set; }
        public int DurationInMonths { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public int DaysOverdue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime NextPayment { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
