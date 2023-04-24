using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HouseRentalAPI.Models;

public partial class HouseRentalDbContext : DbContext
{
    public HouseRentalDbContext()
    {
    }

    public HouseRentalDbContext(DbContextOptions<HouseRentalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HouseRentalPost> HouseRentalPosts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HouseRentalPost>(entity =>
        {
            entity.ToTable("HouseRentalPost");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.ModifiedDate).HasColumnType("date");
            entity.Property(e => e.Price).HasColumnType("decimal(7, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
