using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProfileService.Models;

public partial class ProfileServiceContext : DbContext
{
    public ProfileServiceContext()
    {
    }

    public ProfileServiceContext(DbContextOptions<ProfileServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            DotNetEnv.Env.Load(); 

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Missing CONNECTION_STRING environment variable");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__CAA247C8B37C9D2D");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("address_id");
            entity.Property(e => e.AddressLine)
                .HasMaxLength(255)
                .HasColumnName("address_line");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.District)
                .HasMaxLength(50)
                .HasColumnName("district");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Province).HasColumnName("province");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Ward)
                .HasMaxLength(50)
                .HasColumnName("ward");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FE601C988");

            entity.ToTable("Profile");

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("user_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.PlanExpired)
                .HasColumnType("datetime")
                .HasColumnName("plan_expired");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
