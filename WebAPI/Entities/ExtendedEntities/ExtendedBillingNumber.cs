namespace WebAPI.Entities.ExtendedEntities
{
    public class ExtendedBillingNumber : BillingNumber
    {
        public Customer? Customer { get; set; }
    }
}
