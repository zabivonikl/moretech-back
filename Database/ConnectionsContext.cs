using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MoretechBack.Database.Models;

namespace MoretechBack.Database;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
public sealed class ConnectionsContext : DbContext
{
    public DbSet<User> Users { get; } = null!;

    public ConnectionsContext(DbContextOptions<ConnectionsContext> options) : base(options)
    {
        // Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AchievementConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationStatusConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationStatusRelationConfiguration());
    }
}