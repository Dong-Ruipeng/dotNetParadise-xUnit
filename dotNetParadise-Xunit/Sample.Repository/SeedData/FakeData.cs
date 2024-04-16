using Bogus;
using Sample.Repository.Entities;

namespace Sample.Repository.SeedData;

public class FakeData
{
    public static List<Staff> Staffs = [];

    public static void Init(int count)
    {
        var id = 1;
        var faker = new Faker<Staff>()
            .RuleFor(_ => _.Id, f => id++)
       .RuleFor(u => u.Name, f => f.Person.FullName)
       .RuleFor(u => u.Email, f => f.Person.Email)
       .RuleFor(u => u.Age, f => f.Random.Number(18, 60))
       .RuleFor(u => u.Addresses, f => f.MakeLazy(f.Random.Number(1, 3), () => f.Address.FullAddress()).ToList())
       .RuleFor(u => u.Created, f => f.Date.PastOffset());
        var staffs = faker.Generate(count);
        FakeData.Staffs.AddRange(staffs);
    }
}
