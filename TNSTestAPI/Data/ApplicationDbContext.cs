using Microsoft.EntityFrameworkCore;
using TNSTestAPI.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API configuration
        modelBuilder.Entity<Department>()
            .HasMany(d => d.Users)
            .WithOne(u => u.Department)
            .HasForeignKey(u => u.DepartmentId);
    }
}
