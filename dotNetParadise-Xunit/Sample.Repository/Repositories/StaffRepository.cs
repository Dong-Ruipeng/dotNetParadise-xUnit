using Microsoft.EntityFrameworkCore;
using Sample.Repository.Contexts;
using Sample.Repository.Entities;

namespace Sample.Repository.Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly SampleDbContext _dbContext;
    public DbSet<Staff> dbSet => _dbContext.Set<Staff>();
    public StaffRepository(SampleDbContext dbContext)
    {
        //dbContext.Database.EnsureCreated();
        _dbContext = dbContext;
    }
    public async Task AddStaffAsync(Staff staff, CancellationToken cancellationToken = default)
    {
        await dbSet.AddAsync(staff, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteStaffAsync(int id, CancellationToken cancellationToken = default)
    {
        //await dbSet.AsQueryable().Where(_ => _.Id == id).ExecuteDeleteAsync(cancellationToken);
        var staff = await GetStaffByIdAsync(id, cancellationToken);
        if (staff is not null)
        {
            dbSet.Remove(staff);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateStaffAsync(Staff staff, CancellationToken cancellationToken = default)
    {
        dbSet.Update(staff);
        _dbContext.Entry(staff).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Staff?> GetStaffByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbSet.AsQueryable().Where(_ => _.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Staff>> GetAllStaffAsync(CancellationToken cancellationToken = default)
    {
        return await dbSet.ToListAsync(cancellationToken);
    }

    public async Task BatchAddStaffAsync(List<Staff> staffList, CancellationToken cancellationToken = default)
    {
        await dbSet.AddRangeAsync(staffList, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
