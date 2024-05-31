using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace RSManager.DataBase;

public partial class RepairShopContext : DbContext
{
    public RepairShopContext()
    {
    }

    public RepairShopContext(DbContextOptions<RepairShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autorization> Autorizations { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    public virtual DbSet<WorksPart> WorksParts { get; set; }

    string dbpath = Path.GetFullPath("RepairShop.db").Replace("bin\\Debug\\net8.0-windows\\RepairShop.db", "DataBase\\RepairShop.db");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"datasource={dbpath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autorization>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("Autorization");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(e => e.Birthday).HasColumnType("DATE");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasOne(d => d.SuitableCarNavigation).WithMany(p => p.Parts)
                .HasForeignKey(d => d.SuitableCar)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.HasOne(d => d.CarNavigation).WithMany(p => p.Works)
                .HasForeignKey(d => d.Car)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ClientNavigation).WithMany(p => p.Works)
                .HasForeignKey(d => d.Client)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.WorkerNavigation).WithMany(p => p.Works)
                .HasForeignKey(d => d.Worker)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.Property(e => e.Birthday).HasColumnType("DATE");
        });

        modelBuilder.Entity<WorksPart>(entity =>
        {
            entity.HasKey(e => new { e.WorksId, e.PartId });

            entity.ToTable("Works_Parts");

            entity.HasOne(d => d.Part).WithMany(p => p.WorksParts)
                .HasForeignKey(d => d.PartId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Works).WithMany(p => p.WorksParts)
                .HasForeignKey(d => d.WorksId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
