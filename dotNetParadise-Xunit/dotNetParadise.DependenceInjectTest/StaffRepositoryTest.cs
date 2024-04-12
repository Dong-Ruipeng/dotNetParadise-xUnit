using Sample.Repository.Entities;
using Sample.Repository.Repositories;

namespace dotNetParadise.DependencyInjection;

public class StaffRepositoryTest
{
    private readonly IStaffRepository _staffRepository;
    public StaffRepositoryTest(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }

    //[Fact]
    //public void DependencyInject_WhenCalled_ReturnTrue()
    //{
    //    Assert.True(true);
    //}

    [Fact]
    public async Task AddStaffAsync_WhenCalled_ShouldAddStaffToDatabase()
    {
        // Arrange
        var staff = new Staff { Name = "zhangsan", Email = "zhangsan@163.com" };
        // Act
        await _staffRepository.AddStaffAsync(staff, CancellationToken.None);
        // Assert
        var retrievedStaff = await _staffRepository.GetStaffByIdAsync(staff.Id, CancellationToken.None);
        Assert.NotNull(retrievedStaff); // 确保 Staff 已成功添加到数据库
        Assert.Equal("zhangsan", retrievedStaff.Name); // 检查名称是否正确
    }


    [Fact]
    public async Task DeleteStaffAsync_WhenCalled_ShouldDeleteStaffFromDatabase()
    {

        var staff = new Staff { Name = "John", Email = "john@example.com" };
        await _staffRepository.AddStaffAsync(staff, CancellationToken.None); // 先添加一个 Staff

        // Act
        await _staffRepository.DeleteStaffAsync(staff.Id, CancellationToken.None); // 删除该 Staff

        // Assert
        var retrievedStaff = await _staffRepository.GetStaffByIdAsync(staff.Id, CancellationToken.None); // 尝试获取已删除的 Staff
        Assert.Null(retrievedStaff); // 确保已经删除

    }


    [Fact]
    public async Task UpdateStaffAsync_WhenCalled_ShouldUpdateStaffInDatabase()
    {
        // Arrange
        var staff = new Staff { Name = "John", Email = "john@example.com" };
        await _staffRepository.AddStaffAsync(staff, CancellationToken.None); // 先添加一个 Staff

        // Act
        staff.Name = "Updated Name";
        await _staffRepository.UpdateStaffAsync(staff, CancellationToken.None); // 更新 Staff

        // Assert
        var updatedStaff = await _staffRepository.GetStaffByIdAsync(staff.Id, CancellationToken.None); // 获取已更新的 Staff
        Assert.Equal("Updated Name", updatedStaff?.Name); // 确保 Staff 已更新

    }

    [Fact]
    public async Task GetStaffByIdAsync_WhenCalledWithValidId_ShouldReturnStaffFromDatabase()
    {
        // Arrange
        var staff = new Staff { Name = "John", Email = "john@example.com" };
        await _staffRepository.AddStaffAsync(staff, CancellationToken.None); // 先添加一个 Staff
                                                                             // Act
        var retrievedStaff = await _staffRepository.GetStaffByIdAsync(staff.Id, CancellationToken.None); // 获取 Staff
                                                                                                         // Assert
        Assert.NotNull(retrievedStaff); // 确保成功获取 Staff

    }

    [Fact]
    public async Task GetAllStaffAsync_WhenCalled_ShouldReturnAllStaffFromDatabase()
    {
        // Arrange
        var staff1 = new Staff { Name = "John", Email = "john@example.com" };
        var staff2 = new Staff { Name = "Alice", Email = "alice@example.com" };
        await _staffRepository.AddStaffAsync(staff1, CancellationToken.None); // 先添加 Staff1
        await _staffRepository.AddStaffAsync(staff2, CancellationToken.None); // 再添加 Staff2

        // Act
        var allStaff = await _staffRepository.GetAllStaffAsync(CancellationToken.None); // 获取所有 Staff

        // Assert
        List<Staff> addStaffs = [staff1, staff2];
        Assert.True(addStaffs.All(_ => allStaff.Any(x => x.Id == _.Id))); // 确保成功获取所有 Staff


    }
}