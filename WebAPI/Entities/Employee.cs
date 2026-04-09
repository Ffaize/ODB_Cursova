using WebAPI.Entities.Enums;

namespace WebAPI.Entities
{
    public class Employee : Customer
    {
        public EmployeeRole Role { get; set; }
        public Guid BranchId { get; set; }
        public decimal Salary { get; set; }
        public DateTime HiredAt { get; set; }

    }
}
