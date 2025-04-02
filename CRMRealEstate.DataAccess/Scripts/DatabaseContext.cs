﻿using System.Reflection;
using CRMRealEstate.DataAccess.Configs;
using CRMRealEstate.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CRMRealEstate.DataAccess.Scripts;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<Users> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<UserAnnouncement> UsersAnnouncements { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Adress> Addresses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //original code:
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserConfiguration))!);
        //base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // base.OnModelCreating(modelBuilder);
    }
}