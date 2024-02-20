using Microsoft.EntityFrameworkCore;
using WebTest.Core.Entities;

namespace WebTest.Infrastructure.Database;

public sealed class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Weather> Weather { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Weather>(e =>
        {
            e.HasKey(e => e.Id);
        });

        base.OnModelCreating(modelBuilder);
    }
}
