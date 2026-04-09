namespace WebAPI.Entities.ViewResults
{
    public class TransactionHistoryResult
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CustomerName { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string OperationType { get; set; }
    }
}
