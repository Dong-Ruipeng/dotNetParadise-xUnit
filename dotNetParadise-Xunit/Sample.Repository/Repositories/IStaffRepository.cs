using Microsoft.EntityFrameworkCore;
using Sample.Repository.Entities;

namespace Sample.Repository.Repositories;

public interface IStaffRepository
{
    /// <summary>
    /// 获取 Staff 实体的 DbSet
    /// </summary>
    DbSet<Staff> dbSet { get; }

    /// <summary>
    /// 添加新的 Staff 实体
    /// </summary>
    /// <param name="staff"></param>
    Task AddStaffAsync(Staff staff, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据 Id 删除 Staff 实体
    /// </summary>
    /// <param name="id"></param>
     Task DeleteStaffAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新 Staff 实体
    /// </summary>
    /// <param name="staff"></param>
    Task UpdateStaffAsync(Staff staff, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据 Id 获取单个 Staff 实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Staff?> GetStaffByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取所有 Staff 实体
    /// </summary>
    /// <returns></returns>
    Task<List<Staff>> GetAllStaffAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量更新 Staff 实体
    /// </summary>
    /// <param name="staffList"></param>
    Task BatchAddStaffAsync(List<Staff> staffList, CancellationToken cancellationToken = default);

}
