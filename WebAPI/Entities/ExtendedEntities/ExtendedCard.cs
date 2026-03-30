namespace WebAPI.Entities.ExtendedEntities
{
    public class ExtendedCard : Card
    {
        public ExtendedBillingNumber? BillingNumber { get; set; }
        public Customer? Customer { get; set; }
    }
}
