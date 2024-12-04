using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<PlatformStatus> PlatformStatuses { get; set; }
    public DbSet<CallLog> CallLogs { get; set; } // DbSet para almacenar el contador de llamadas

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}

