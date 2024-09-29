using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ModelDto.Models;

public partial class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codice)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("codice");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Customermobile)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("customermobile");
            entity.Property(e => e.Customername)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("customername");
            entity.Property(e => e.IsCompleted)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bit(1)")
                .HasColumnName("isCompleted");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("timestamp")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_details");

            entity.HasIndex(e => e.OrderId, "order_details_orders_FK");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'0.00'")
                .HasColumnType("double(8,2)")
                .HasColumnName("price");
            entity.Property(e => e.Qty)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("qty");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("timestamp")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("order_details_orders_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
