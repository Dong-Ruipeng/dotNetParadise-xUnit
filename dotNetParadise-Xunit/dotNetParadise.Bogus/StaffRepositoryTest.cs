using Bogus;
using Sample.Repository.Entities;
using Sample.Repository.Repositories;

namespace dotNetParadise.Bogus;

public class StaffRepositoryTest
{
    private readonly IStaffRepository _staffRepository;
    public StaffRepositoryTest(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }

    [Fact]
    public async Task BatchAddStaffAsync_WhenCalled_ShouldAddStaffToDatabase()
    {
        // Arrange
        var faker = new Faker<Staff>()
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Age, f => f.Random.Number(18, 60))
            .RuleFor(u => u.Addresses, f => f.MakeLazy(f.Random.Number(1, 3), () => f.Address.FullAddress()).ToList())
            .RuleFor(u => u.Created, f => f.Date.PastOffset());
        var staffs = faker.Generate(500);
        // Act
        await _staffRepository.BatchAddStaffAsync(staffs, CancellationToken.None);
        // Assert
        var retrievedStaffs = await _staffRepository.GetAllStaffAsync(CancellationToken.None);
        Assert.NotNull(retrievedStaffs); // 确保 Staff 已成功添加到数据库
        Assert.True(staffs.All(x => retrievedStaffs.Any(_ => x.Id == _.Id)));
    }

}