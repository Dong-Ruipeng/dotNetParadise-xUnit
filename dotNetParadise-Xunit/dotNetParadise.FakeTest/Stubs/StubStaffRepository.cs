using Microsoft.EntityFrameworkCore;
using Sample.Repository.Entities;
using Sample.Repository.Repositories;

namespace dotNetParadise.FakeTest.Stubs;

public class StubStaffRepository : IStaffRepository
{
    public DbSet<Staff> dbSet => default!;

    public async Task AddStaffAsync(Staff staff, CancellationToken cancellationToken)
    {
        // 模拟添加员工操作
        await Task.CompletedTask;
    }

    public async Task DeleteStaffAsync(int id)
    {
        // 模拟删除员工操作
        await Task.CompletedTask;
    }

    public async Task UpdateStaffAsync(Staff staff, CancellationToken cancellationToken)
    {
        // 模拟更新员工操作
        await Task.CompletedTask;
    }

    public async Task<Staff?> GetStaffByIdAsync(int id, CancellationToken cancellationToken)
    {
        // 模拟根据 ID 获取员工操作
        return await Task.FromResult(new Staff { Id = id, Name = "Mock Staff" });
    }

    public async Task<List<Staff>> GetAllStaffAsync(CancellationToken cancellationToken)
    {
        // 模拟获取所有员工操作
        return await Task.FromResult(new List<Staff> { new Staff { Id = 1, Name = "Mock Staff 1" }, new Staff { Id = 2, Name = "Mock Staff 2" } });
    }

    public async Task BatchAddStaffAsync(List<Staff> staffList, CancellationToken cancellationToken)
    {
        // 模拟批量添加员工操作
        await Task.CompletedTask;
    }

    public async Task DeleteStaffAsync(int id, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}
