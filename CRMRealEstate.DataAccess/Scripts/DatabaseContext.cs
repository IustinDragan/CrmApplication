using System.Reflection;
using CRMRealEstate.DataAccess.Configs;
using CRMRealEstate.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRMRealEstate.DataAccess.Scripts;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Users> Users { get; set; }

    public DbSet<Company> Company { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserConfiguration))!);
        base.OnModelCreating(modelBuilder);
    }
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Users>()
    //         .HasOne(u => u.Company)
    //         .WithMany(c => c.Users)
    //         .HasForeignKey(u => u.CompanyId)
    //         .OnDelete(DeleteBehavior.Cascade);
    //
    //     modelBuilder.Entity<Users>()
    //         .HasOne(u => u.Roles)
    //         .WithMany(r => r.Users)
    //         .HasForeignKey(u => u.RoleId)
    //         .OnDelete(DeleteBehavior.Restrict);
    // }
}