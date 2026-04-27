using Microsoft.EntityFrameworkCore;
using PollsApi.Models;
using System.Reflection.Emit;

namespace PollsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.PollId, v.SessionId })
                .IsUnique();
        }
    }
}