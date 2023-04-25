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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=HouseRentalDb;Trusted_Connection=True;TrustServerCertificate=True;User ID=sa;Password=Fmcs@23145632");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HouseRentalPost>(entity =>
        {
            entity.ToTable("HouseRentalPost");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(7, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
