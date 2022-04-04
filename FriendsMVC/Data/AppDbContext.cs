using FriendsMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Countryes> Countryes { get; set; }
        public DbSet<Fellow> Fellows { get; set; }
        public DbSet<Fellowship> Fellowships { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:milioliserver.database.windows.net,1433;Initial Catalog=TP03;Persist Security Info=False;User ID=milioli85;Password=Francisco85!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Fellowship>()
                .HasOne(p => p.Friends)
                .WithMany(f => f.FellowshipOfTheRing)
                .HasForeignKey(pi => pi.FriendId);

            builder.Entity<Fellowship>()
                .HasOne(fr => fr.Fellow)
                .WithMany(f => f.Fellowship)
                .HasForeignKey(fi => fi.FellowId);

            base.OnModelCreating(builder);
        }
    }
}
