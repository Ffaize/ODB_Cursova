namespace WebAPI.Entities.ViewResults
{
    public class AnomalyResult
    {
        public Guid Id { get; set; }
        public DateTime EventDate { get; set; }
        public string AnomalyType { get; set; }
        public string Severity { get; set; }
        public decimal Amount { get; set; }
        public string CustomerId { get; set; }
        public string Description { get; set; }
    }
}
