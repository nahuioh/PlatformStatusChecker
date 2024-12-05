using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<PlatformStatus> PlatformStatuses { get; set; }
    public required DbSet<ApiCallCounter> ApiCallCounters { get; set; } // Nuevo DbSet
}