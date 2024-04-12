using Microsoft.EntityFrameworkCore;
using Sample.Repository.Entities;

namespace Sample.Repository.Contexts;

public class SampleDbContext(DbContextOptions<SampleDbContext> options) : DbContext(options)
{
    public DbSet<Staff> Staff { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
