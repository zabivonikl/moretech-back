﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MoretechBack.Database.Models;
#pragma warning disable CS8618

namespace MoretechBack.Database;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
public sealed class ConnectionsContext : DbContext
{
    public DbSet<User> Users { get; private set; }

    public ConnectionsContext(DbContextOptions<ConnectionsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<Notification>()
            .HasMany(p => p.NotificationStatus)
            .WithMany(p => p.Notifications)
            .UsingEntity(j => j.ToTable("NotificationStatusRelation"));
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AchievementConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationStatusConfiguration());
    }
}