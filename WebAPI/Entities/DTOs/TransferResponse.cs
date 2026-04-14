namespace WebAPI.Entities.DTOs
{
    public class TransferResponse
    {
        public Guid TransactionId { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
    }
}
