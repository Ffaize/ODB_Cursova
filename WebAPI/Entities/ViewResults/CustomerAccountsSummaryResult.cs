namespace WebAPI.Entities.ViewResults
{
    public class CustomerAccountsSummaryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int AccountCount { get; set; }
        public decimal TotalBalance { get; set; }
        public int CardCount { get; set; }
        public int ActiveCredits { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
