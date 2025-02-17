using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MIS.Services.Project.Api.Models;

public partial class ProjectdbContext : DbContext
{
    private readonly IConfiguration _iconfig;

    //public ProjectdbContext(IConfiguration Iconfig)
    //{
    //    _Iconfig = Iconfig;
    //}

    public ProjectdbContext()
    {

    }

    public ProjectdbContext(DbContextOptions<ProjectdbContext> options, IConfiguration iconfig)
        : base(options)
    {
        _iconfig = iconfig;
    }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectResource> ProjectResources { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Vertical> Verticals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_iconfig.GetConnectionString("sqlConnection"));
        }
    }
    //=> optionsBuilder.UseSqlServer(_Iconfig.GetConnectionString("sqlConnection"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABEF0EF120128");

            entity.Property(e => e.ProjectId).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.ProjectName).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Vertical).WithMany(p => p.Projects)
                .HasForeignKey(d => d.VerticalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Projects__Vertic__3B75D760");
        });

        modelBuilder.Entity<ProjectResource>(entity =>
        {
            entity.HasKey(e => e.ResourceId).HasName("PK__ProjectR__4ED1816F8B009ACA");

            entity.Property(e => e.ResourceId).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectResources)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectRe__Proje__3E52440B");

            entity.HasOne(d => d.Role).WithMany(p => p.ProjectResources)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectRe__RoleI__3F466844");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A83992ABB");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Vertical>(entity =>
        {
            entity.HasKey(e => e.VerticalId).HasName("PK__Vertical__941FC6B064AE7210");

            entity.ToTable("Vertical");

            entity.Property(e => e.VerticalId).ValueGeneratedNever();
            entity.Property(e => e.VerticalName)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
