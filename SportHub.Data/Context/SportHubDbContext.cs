using Microsoft.EntityFrameworkCore;
using SportHub.Core.Entities;
using SportHub.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportHub.Data.Context
{
    public class SportHubDbContext : DbContext
    {
        public SportHubDbContext(DbContextOptions<SportHubDbContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
         .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Player>()
                .Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Player>()
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Player || e.Entity is Team);

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((dynamic)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    ((dynamic)entityEntry.Entity).UpdatedAt = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Player || e.Entity is Team);

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((dynamic)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    ((dynamic)entityEntry.Entity).UpdatedAt = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
