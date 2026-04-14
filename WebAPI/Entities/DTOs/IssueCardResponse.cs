namespace WebAPI.Entities.DTOs
{
    public class IssueCardResponse
    {
        public Guid CardId { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CardType { get; set; }
    }
}
