namespace WebAPI.Entities.ViewResults
{
    public class BranchPerformanceResult
    {
        public Guid Id { get; set; }
        public string BranchName { get; set; }
        public string Location { get; set; }
        public int EmployeeCount { get; set; }
        public int CustomerCount { get; set; }
        public decimal TotalBalance { get; set; }
        public int ActiveCredits { get; set; }
        public decimal AvgSalary { get; set; }
        public decimal TotalSalaryExpense { get; set; }
        public int RecentTransactions { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
