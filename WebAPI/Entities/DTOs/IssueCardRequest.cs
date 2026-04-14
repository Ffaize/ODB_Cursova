namespace WebAPI.Entities.DTOs
{
    public class IssueCardRequest
    {
        public Guid BillingNumberId { get; set; }
        public int CardType { get; set; }
    }
}
