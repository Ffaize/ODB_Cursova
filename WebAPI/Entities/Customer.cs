using WebAPI.Entities.Enums;

namespace WebAPI.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid BillingNumberId { get; set; }
    }
}
