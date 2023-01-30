using Microsoft.EntityFrameworkCore;

namespace BynetApi.Models;

public partial class BynetContext : DbContext
{
    public BynetContext()
    {
    }

    public BynetContext(DbContextOptions<BynetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeDto> Employees { get; set; }

    public virtual DbSet<V_Employee> V_Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("BynetDb");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeDto>(entity =>
        {
            entity.HasKey(e => e.IdNumber).HasName("PK__EmployeeId");

            entity.ToTable(tb => tb.HasTrigger("Employees_OnDeleteSetNull"));

        });

        modelBuilder.Entity<V_Employee>(entity =>
        {
            entity.ToView("V_Employees");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
