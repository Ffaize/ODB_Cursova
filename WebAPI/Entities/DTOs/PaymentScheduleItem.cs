namespace WebAPI.Entities.DTOs
{
    public class PaymentScheduleItem
    {
        public int PaymentNumber { get; set; }
        public decimal MonthlyPayment { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public decimal RemainingAmount { get; set; }
    }
}
