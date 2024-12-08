using System;
using System.Collections.Generic;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistent;

public class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }

    public virtual DbSet<VehicleDetails> VehicleDetails { get; set; }
    
    public virtual DbSet<CommissionDetails> CommissionDetails { get; set; }

    public virtual DbSet<MonthlySaleDetails> MonthlySaleDetails { get; set; }

    public virtual DbSet<SalesmanDetails> SalesmanDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VehicleDetails>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_VehicleDetail_Id");
        });
        
        modelBuilder.Entity<CommissionDetails>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CommissionDetails_Id");
        });

        modelBuilder.Entity<MonthlySaleDetails>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MonthlySaleDetails_Id");

            entity.HasOne(d => d.Salesman).WithMany(p => p.MonthlySaleDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MonthlySaleDetails_SalesmanId_SalesmanDetails_Id");
        });

        modelBuilder.Entity<SalesmanDetails>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SalesmanDetails_Id");
        });
    }
}
