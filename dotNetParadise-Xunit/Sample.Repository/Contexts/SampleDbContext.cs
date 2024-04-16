using Microsoft.EntityFrameworkCore;
using Sample.Repository.Entities;
using Sample.Repository.SeedData;

namespace Sample.Repository.Contexts;

public class SampleDbContext(DbContextOptions<SampleDbContext> options) : DbContext(options)
{
    public DbSet<Staff> Staffs { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //FakeData.Init(1000);
        //builder.Entity<Staff>().HasData(FakeData.Staffs);
    }
}
