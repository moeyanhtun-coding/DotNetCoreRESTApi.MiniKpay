using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreRESTApi.MiniKpay.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblCustomerBalance> TblCustomerBalances { get; set; }

    public virtual DbSet<TblCustomerTransactionHistory> TblCustomerTransactionHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
        {
            
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.ToTable("Tbl_Customer");

            entity.Property(e => e.CustomerCode)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblCustomerBalance>(entity =>
        {
            entity.HasKey(e => e.CustomerBalanceId);

            entity.ToTable("Tbl_CustomerBalance");

            entity.Property(e => e.Balance).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblCustomerTransactionHistory>(entity =>
        {
            entity.HasKey(e => e.CustomerTransactionHistoryId);

            entity.ToTable("Tbl_CustomerTransactionHistory");

            entity.Property(e => e.Amount).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.FromMobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ToMobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
