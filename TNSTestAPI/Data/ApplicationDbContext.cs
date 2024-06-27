using Microsoft.EntityFrameworkCore;
using TNSTestAPI.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> departments { get; set; }
    public DbSet<User> users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Department>()
            .HasKey(d => d.department_id);
        modelBuilder.Entity<User>()
            .HasKey(u => u.user_id);
    }
}
