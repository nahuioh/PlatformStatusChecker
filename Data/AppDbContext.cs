using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<PlatformStatus> PlatformStatuses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
