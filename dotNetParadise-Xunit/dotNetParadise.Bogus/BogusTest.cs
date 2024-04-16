using Bogus;
using Bogus.DataSets;
using Newtonsoft.Json;
using Sample.Repository.Entities;
using Xunit.DependencyInjection;

namespace dotNetParadise.Bogus;

public class BogusTest(ITestOutputHelperAccessor testOutputHelperAccessor)
{
    [Theory]
    [InlineData(null)]
    [InlineData("zh_CN")]
    public void Locales_ConfigTest(string? locale)
    {
        //default
        var faker = locale is null ? new Faker<Staff>() : new Faker<Staff>(locale);

        faker.RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Age, f => f.Random.Number(18, 60))
            .RuleFor(u => u.Addresses, f => f.MakeLazy(f.Random.Number(1, 3), () => f.Address.FullAddress()).ToList())
            .RuleFor(u => u.Created, f => f.Date.PastOffset());
        var staff = faker.Generate();
        var consoleType = locale is null ? "default" : locale;
        testOutputHelperAccessor.Output?.WriteLine($"{consoleType}:{JsonConvert.SerializeObject(staff)}");
    }


    [Fact]
    public void Bogus_Compare_SeedTest()
    {
        // Arrange
        var faker = new Faker<Staff>()
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Age, f => f.Random.Number(18, 60))
            .RuleFor(u => u.Addresses, f => f.MakeLazy(f.Random.Number(1, 3), () => f.Address.FullAddress()).ToList())
            .RuleFor(u => u.Created, f => f.Date.PastOffset());

        // Act
        var staffs1 = Enumerable.Range(1, 5)
            .Select(_ => faker.UseSeed(_).Generate())
            .ToList();

        OutputStaffInformation(staffs1, "第一次");

        var staffs2 = Enumerable.Range(1, 5)
            .Select(_ => faker.UseSeed(_).Generate())
            .ToList();

        OutputStaffInformation(staffs2, "第二次");

        // Assert
        Assert.True(staffs1.All(staff1 => staffs2.Any(staff2 => staff1.Name == staff2.Name && staff1.Email == staff2.Email)));
    }

    private void OutputStaffInformation(List<Staff> staffs, string iteration)
    {
        foreach (Staff staff in staffs)
        {
            testOutputHelperAccessor.Output?.WriteLine($"{iteration}: name: {staff.Name}, email: {staff.Email}");
        }
    }
}
