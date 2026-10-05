using Microsoft.EntityFrameworkCore;

namespace Soccer.Models;

public class SoccerContext : DbContext
{
    public SoccerContext(DbContextOptions<SoccerContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    // Використання сучасного підходу Set<T>() гарантує, що DbSet ніколи не буде null
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Team> Teams => Set<Team>();
}