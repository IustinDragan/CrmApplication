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
}