namespace WebAPI.Entities.ViewResults
{
    public class CustomerLoanRiskResult
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public int TotalLoans { get; set; }
        public decimal TotalLoanAmount { get; set; }
        public decimal TotalOutstanding { get; set; }
        public decimal DebtToIncomeRatio { get; set; }
        public string RiskLevel { get; set; }
        public int OverdueLoans { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
}
