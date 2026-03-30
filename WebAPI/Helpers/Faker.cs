using Bogus;
using WebAPI.Entities;

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
                .RuleFor(c => c.CreatedAt, f => f.Date.Past());

            var employees = faker.Generate(count);

            return employees ?? [];
        }
    }
}
