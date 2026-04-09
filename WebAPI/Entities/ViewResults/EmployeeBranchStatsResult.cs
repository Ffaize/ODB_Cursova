namespace WebAPI.Entities.ViewResults
{
    public class EmployeeBranchStatsResult
    {
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public string Location { get; set; }
        public int EmployeeCount { get; set; }
        public decimal AverageSalary { get; set; }
        public decimal TotalSalaries { get; set; }
        public int TopRoleEmployeeCount { get; set; }
        public string TopRole { get; set; }
        public DateTime BranchCreatedAt { get; set; }
    }
}
