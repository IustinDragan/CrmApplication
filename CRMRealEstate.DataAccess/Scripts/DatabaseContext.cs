﻿using System.Reflection;
using CRMRealEstate.DataAccess.Configs;
using CRMRealEstate.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRMRealEstate.DataAccess.Scripts;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Users> Users { get; set; }
    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserConfiguration))!);
        base.OnModelCreating(modelBuilder);
        
        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // base.OnModelCreating(modelBuilder);
    }
}