using Microsoft.EntityFrameworkCore;
using Superplay.Models;

namespace Superplay.Data;

public class ApplicationDbContext : DbContext
{
    #pragma warning disable CS8618 //DBSets are null because they are injected by ASP.NET
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Device> Devices { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>().ToTable("Player");
        modelBuilder.Entity<Device>().ToTable("Devices");
    }
}