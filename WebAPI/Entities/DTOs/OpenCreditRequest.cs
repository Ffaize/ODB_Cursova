namespace WebAPI.Entities.DTOs
{
    public class OpenCreditRequest
    {
        public Guid CustomerId { get; set; }
        public decimal FullAmount { get; set; }
        public int DurationInMonths { get; set; }
        public string Currency { get; set; } = "UAH";
    }
}
