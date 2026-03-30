namespace WebAPI.Entities.ExtendedEntities
{
    public class ExtendedBillingOperation : BillingOperation
    {
        public ExtendedBillingNumber? BillingNumberFrom { get; set; }
        public ExtendedBillingNumber? BillingNumberTo { get; set; }
        public ExtendedCredit? Credit { get; set; }
    }
}
