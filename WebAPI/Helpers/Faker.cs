using Bogus;
using WebAPI.Entities;
using WebAPI.Entities.Enums;

namespace WebAPI.Helpers
{
    public static class Faker
    {
        public static List<Customer> GenerateMockCustomers(int count = 1)
        {
            var faker = new Faker<Customer>("en")
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Name.FirstName())
                .RuleFor(c => c.Surname, f => f.Name.LastName())
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Name, c.Surname))
                .RuleFor(c => c.PasswordHash, f => f.Internet.Password())
                .RuleFor(c => c.CreatedAt, f => f.Date.Between(DateTime.Parse("2020-01-01"), DateTime.Parse("2024-12-31")));

            return faker.Generate(count) ?? [];
        }

        public static List<Employee> GenerateMockEmployees(int count = 1, List<Guid>? branchIds = null)
        {
            // If no branch IDs provided, generate random (old behavior)
            branchIds ??= Enumerable.Range(0, Math.Max(1, count)).Select(_ => Guid.NewGuid()).ToList();
            
            var faker = new Faker<Employee>("en")
                .RuleFor(e => e.Id, f => Guid.NewGuid())
                .RuleFor(e => e.Name, f => f.Name.FirstName())
                .RuleFor(e => e.Surname, f => f.Name.LastName())
                .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.Name, e.Surname))
                .RuleFor(e => e.PasswordHash, f => f.Internet.Password())
                .RuleFor(e => e.CreatedAt, f => f.Date.Between(DateTime.Parse("2020-01-01"), DateTime.Parse("2024-12-31")))
                .RuleFor(e => e.Role, f => f.PickRandom<EmployeeRole>())
                .RuleFor(e => e.BranchId, f => f.PickRandom(branchIds))
                .RuleFor(e => e.Salary, f => (decimal)f.Random.Double(1000, 10000))
                .RuleFor(e => e.HiredAt, f => f.Date.Between(DateTime.Parse("2020-01-01"), DateTime.Parse("2024-12-31")));

            return faker.Generate(count) ?? [];
        }

        public static List<Branch> GenerateMockBranches(int count = 1)
        {
            var faker = new Faker<Branch>("en")
                .RuleFor(b => b.Id, f => Guid.NewGuid())
                .RuleFor(b => b.Name, f => f.Company.CompanyName())
                .RuleFor(b => b.NumberOfEmployees, f => f.Random.Int(0, 50))
                .RuleFor(b => b.NumberOfBranch, f => f.Random.Int(1, 20))
                .RuleFor(b => b.Location, f => $"{f.Address.City()}, {f.Address.Country()}")
                .RuleFor(b => b.ContactEmail, f => f.Internet.Email())
                .RuleFor(b => b.ContactPhone, f => f.Phone.PhoneNumber())
                .RuleFor(b => b.CreatedAt, f => f.Date.Between(DateTime.Parse("2020-01-01"), DateTime.Parse("2024-12-31")));

            return faker.Generate(count) ?? [];
        }

        public static List<Card> GenerateMockCards(int count = 1, List<Guid>? billingNumberIds = null, List<Guid>? customerIds = null)
        {
            // If no IDs provided, generate random (old behavior)
            billingNumberIds ??= Enumerable.Range(0, Math.Max(1, count)).Select(_ => Guid.NewGuid()).ToList();
            customerIds ??= Enumerable.Range(0, Math.Max(1, count)).Select(_ => Guid.NewGuid()).ToList();
            
            var faker = new Faker<Card>("en")
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.CardNumber, f => f.Finance.CreditCardNumber())
                .RuleFor(c => c.Status, f => f.PickRandom<CardStatus>())
                .RuleFor(c => c.CardHolderName, f => f.Name.FullName())
                .RuleFor(c => c.LaunchDate, f => f.Date.Between(DateTime.Parse("2020-01-01"), DateTime.Parse("2024-12-31")))
                .RuleFor(c => c.ExpirationDate, f => f.Date.Between(DateTime.Parse("2025-01-01"), DateTime.Parse("2030-12-31")))
                .RuleFor(c => c.Cvv, f => f.Random.Int(100, 999))
                .RuleFor(c => c.BillingNumberId, f => f.PickRandom(billingNumberIds))
                .RuleFor(c => c.CustomerId, f => f.PickRandom(customerIds));

            return faker.Generate(count) ?? [];
        }

        public static List<Credit> GenerateMockCredits(int count = 1, List<Guid>? billingNumberIds = null)
        {
            // If no billing number IDs provided, generate random (old behavior)
            billingNumberIds ??= Enumerable.Range(0, Math.Max(1, count)).Select(_ => Guid.NewGuid()).ToList();
            
            var faker = new Faker<Credit>("en")
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FullAmount, f => (decimal)f.Random.Double(5000, 500000))
                .RuleFor(c => c.RemainingToPay, (f, c) => (decimal)f.Random.Double(0, (double)c.FullAmount))
                .RuleFor(c => c.MonthlyPayment, (f, c) => c.FullAmount / (decimal)f.Random.Int(12, 360))
                .RuleFor(c => c.DurationInMonths, f => f.Random.Int(12, 360))
                .RuleFor(c => c.Currency, f => f.PickRandom(new[] { "USD", "EUR", "UAH" }))
                .RuleFor(c => c.CreatedAt, f => f.Date.Between(DateTime.Parse("2020-01-01"), DateTime.Parse("2024-12-31")))
                .RuleFor(c => c.NextPayment, f => f.Date.Between(DateTime.Now, DateTime.Now.AddDays(90)))
                .RuleFor(c => c.LastPayment, f => f.Date.Between(DateTime.Now.AddDays(-60), DateTime.Now))
                .RuleFor(c => c.ClosedAt, f => f.Random.Bool() ? f.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now) : null)
                .RuleFor(c => c.IsClosed, (f, c) => c.ClosedAt.HasValue)
                .RuleFor(c => c.BillingNumberId, f => f.PickRandom(billingNumberIds));

            return faker.Generate(count) ?? [];
        }

        public static List<BillingNumber> GenerateMockBillingNumbers(int count = 1, List<Guid>? customerIds = null)
        {
            // If no customer IDs provided, generate random (old behavior)
            customerIds ??= Enumerable.Range(0, Math.Max(1, count)).Select(_ => Guid.NewGuid()).ToList();
            
            var faker = new Faker<BillingNumber>("en")
                .RuleFor(b => b.Id, f => Guid.NewGuid())
                .RuleFor(b => b.AccountNumber, f => f.Finance.Iban())
                .RuleFor(b => b.Balance, f => (decimal)f.Random.Double(0, 100000))
                .RuleFor(b => b.Currency, f => f.PickRandom(new[] { "USD", "EUR", "UAH" }))
                .RuleFor(b => b.AccountType, f => f.PickRandom<AccountType>())
                .RuleFor(b => b.Status, f => f.PickRandom<AccountStatus>())
                .RuleFor(b => b.CreatedAt, f => f.Date.Between(DateTime.Parse("2020-01-01"), DateTime.Parse("2024-12-31")))
                .RuleFor(b => b.UpdatedAt, f => f.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now))
                .RuleFor(b => b.CustomerId, f => f.PickRandom(customerIds));

            return faker.Generate(count) ?? [];
        }

        public static List<BillingOperation> GenerateMockBillingOperations(int count = 1, List<Guid>? billingNumberIds = null, List<Guid>? creditIds = null)
        {
            // If no billing number IDs provided, generate random (old behavior)
            billingNumberIds ??= Enumerable.Range(0, Math.Max(1, count)).Select(_ => Guid.NewGuid()).ToList();
            // If no credit IDs provided, create empty list (will use null when needed)
            creditIds ??= [];
            
            var faker = new Faker<BillingOperation>("en")
                .RuleFor(bo => bo.Id, f => Guid.NewGuid())
                .RuleFor(bo => bo.Amount, f => (decimal)f.Random.Double(100, 10000))
                .RuleFor(bo => bo.Currency, f => f.PickRandom(new[] { "USD", "EUR", "UAH" }))
                .RuleFor(bo => bo.CreatedAt, f => f.Date.Between(DateTime.Parse("2020-01-01"), DateTime.Parse("2024-12-31")))
                .RuleFor(bo => bo.Description, f => f.Lorem.Sentence())
                .RuleFor(bo => bo.PaymentPurpose, f => f.PickRandom<PaymentPurpose>())
                .RuleFor(bo => bo.CustomerId, f => f.Random.Bool() ? Guid.NewGuid() : null)
                .RuleFor(bo => bo.BillingNumberIdFrom, f => f.Random.Bool() ? f.PickRandom(billingNumberIds) : null)
                .RuleFor(bo => bo.BillingNumberIdTo, f => f.Random.Bool() ? f.PickRandom(billingNumberIds) : null)
                .RuleFor(bo => bo.CreditId, f => creditIds.Count > 0 && f.Random.Bool() ? f.PickRandom(creditIds) : null);

            return faker.Generate(count) ?? [];
        }

        public static List<ActionLog> GenerateMockActionLogs(int count = 1)
        {
            var faker = new Faker<ActionLog>("en")
                .RuleFor(al => al.Id, f => Guid.NewGuid())
                .RuleFor(al => al.Description, f => f.Lorem.Sentence())
                .RuleFor(al => al.Operation, f => f.PickRandom(new[] { "CREATE", "READ", "UPDATE", "DELETE" }))
                .RuleFor(al => al.CreatedAt, f => f.Date.Between(DateTime.Parse("2020-01-01"), DateTime.Parse("2024-12-31")))
                .RuleFor(al => al.CreatedBy, f => f.Name.FullName());

            return faker.Generate(count) ?? [];
        }
    }
}
