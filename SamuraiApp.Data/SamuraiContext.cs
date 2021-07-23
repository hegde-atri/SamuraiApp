﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamuraiApp.Domain;
using System;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; } 
        public DbSet<Battle> Battles { get; set; }

        public SamuraiContext(DbContextOptions<SamuraiContext> options) :base(options)
        {

        }

        public SamuraiContext(DbContextOptions opt) : base(opt)
        {
            
        }

        public SamuraiContext()
        {
          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=SamuraiTestData");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
             .HasMany(s => s.Battles)
             .WithMany(b => b.Samurais)
             .UsingEntity<BattleSamurai>
              (bs => bs.HasOne<Battle>().WithMany(),
               bs => bs.HasOne<Samurai>().WithMany())
             .Property(bs => bs.DateJoined)
             .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Horse>().ToTable("Horses");
        }
    }
}
