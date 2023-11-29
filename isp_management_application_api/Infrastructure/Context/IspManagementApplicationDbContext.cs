using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class IspManagementApplicationDbContext : DbContext
{
    public IspManagementApplicationDbContext(DbContextOptions<IspManagementApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppFeature> AppFeatures { get; set; }

    public virtual DbSet<Right> Rights { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleRight> RoleRights { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppFeature>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FeatureName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Right>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.RightsName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.AppFeature).WithMany(p => p.Rights)
                .HasForeignKey(d => d.AppFeatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rights_AppFeatures");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletationTime).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleRight>(entity =>
        {
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletationTime).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletationTime).HasColumnType("datetime");
            entity.Property(e => e.LastModificationTime).HasColumnType("datetime");
            entity.Property(e => e.LoginName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(64);
            entity.Property(e => e.PasswordSalt).HasMaxLength(128);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserMobileNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
