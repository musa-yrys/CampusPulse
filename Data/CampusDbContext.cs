using CampusPulse.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusPulse.Data;

public class CampusDbContext(DbContextOptions<CampusDbContext> options) : DbContext(options)
{
    public DbSet<CampusEvent> Events => Set<CampusEvent>();
    public DbSet<EventRegistration> Registrations => Set<EventRegistration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CampusEvent>().HasIndex(e => e.StartsAt);
        modelBuilder.Entity<EventRegistration>()
            .HasOne(r => r.CampusEvent).WithMany(e => e.Registrations)
            .HasForeignKey(r => r.CampusEventId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<EventRegistration>().HasIndex(r => new { r.CampusEventId, r.Email }).IsUnique();
    }
}
