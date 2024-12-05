using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<PlatformStatus> PlatformStatuses { get; set; }
    public DbSet<ApiCallCounter> ApiCallCounters { get; set; } // Nuevo DbSet

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}