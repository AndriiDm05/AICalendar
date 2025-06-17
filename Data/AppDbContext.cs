using AICalendar.Models;
using Microsoft.EntityFrameworkCore;

namespace AICalendar.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Participant> Participants { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participant>()
            .HasOne(p => p.User)
            .WithMany(u => u.Participations)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Participant>()
            .HasOne(p => p.Event)
            .WithMany(e => e.Participants)
            .HasForeignKey(p => p.EventId);
    }
}
