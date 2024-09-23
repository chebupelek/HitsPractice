using Microsoft.EntityFrameworkCore;
using Events.TokenDbModels;

namespace Events.Contexts;

public class LogContext : DbContext
{
    public LogContext(DbContextOptions<LogContext> options) : base(options) { }

    public DbSet<LogDbModel> Log { get; set; }
}
