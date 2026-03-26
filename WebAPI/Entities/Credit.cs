namespace WebAPI.Entities
{
    public class Credit
    {
        public Guid Id { get; set; }
        public decimal FullAmount { get; set; }
        public decimal RemainingToPay { get; set; }
        public decimal MonthlyPayment { get; set; }
        public int DurationInMonths { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime NextPayment { get; set; }
        public DateTime? LastPaid { get; set; }
        public DateTime? ClosedAt { get; set; }
        public bool IsClosed { get; set; }
        public Guid UserId { get; set; }
    }
}
