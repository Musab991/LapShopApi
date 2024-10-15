using System;
using System.Collections.Generic;
using BuisnessLibrary.Bl.Account;
using DomainLibrary.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BuisnessLibrary.Data;

public partial class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext()
    {
    }
  
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbGpu> TbGpus { get; set; }

    public virtual DbSet<TbHardDisk> TbHardDisks { get; set; }


    public virtual DbSet<TbItem> TbItems { get; set; }


    public virtual DbSet<TbItemImage> TbItemImages { get; set; }

    public virtual DbSet<TbItemType> TbItemTypes { get; set; }

    public virtual DbSet<TbO> TbOs { get; set; }

    public virtual DbSet<TbProcessor> TbProcessors { get; set; }



    public virtual DbSet<TbRam> TbRams { get; set; }

    public virtual DbSet<TbSalesInvoice> TbSalesInvoices { get; set; }

    public virtual DbSet<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; }

    public virtual DbSet<TbScreenResolution> TbScreenResolutions { get; set; }


   

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    //     var Configuration = new ConfigurationBuilder()
    //    .AddJsonFile("appsettings.json")
    //    .Build();

    //    var constr = Configuration.GetSection("constr").Value;

    //    optionsBuilder.UseSqlServer(constr)
    //        .AddInterceptors(new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");


        modelBuilder.Entity<TbCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValue("");
            entity.Property(e => e.ImageName)
                .HasMaxLength(200)
                .HasDefaultValue("");
            entity.Property(e => e.UpdatedBy).HasMaxLength(200);
        });

     

        modelBuilder.Entity<TbGpu>(entity =>
        {
            entity.HasKey(e => e.GpuId);

            entity.ToTable("TbGPUs");

            entity.Property(e => e.GpuId).HasColumnName("GPUId");
            entity.Property(e => e.GpuName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("GPUName");
        });

        modelBuilder.Entity<TbHardDisk>(entity =>
        {
            entity.HasKey(e => e.HardDiskId);

            entity.Property(e => e.HardDiskName)
                .HasMaxLength(250)
                .IsUnicode(false);
        });


        modelBuilder.Entity<TbItem>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.HasIndex(e => e.GpuId, "IX_TbItems_GPUId");

            entity.HasIndex(e => e.HardDiskId, "IX_TbItems_HardDiskId");

            entity.HasIndex(e => e.ItemTypeId, "IX_TbItems_ItemTypeId");

            entity.HasIndex(e => e.OsId, "IX_TbItems_OsId");

            entity.HasIndex(e => e.ProcessorId, "IX_TbItems_ProcessorId");

            entity.HasIndex(e => e.RamId, "IX_TbItems_RAMId");

            entity.HasIndex(e => e.ScreenResolutionId, "IX_TbItems_ScreenResolutionId");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValue("");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.GpuId).HasColumnName("GPUId");
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.RamId).HasColumnName("RAMId");
            entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.ScreenSize).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(200);
            entity.Property(e => e.Weight).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbItems_TbCategories");

            entity.HasOne(d => d.Gpu).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.GpuId)
                .HasConstraintName("FK_TbItems_TbGPUs");

            entity.HasOne(d => d.HardDisk).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.HardDiskId)
                .HasConstraintName("FK_TbItems_TbHardDisks");

            entity.HasOne(d => d.ItemType).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.ItemTypeId)
                .HasConstraintName("FK_TbItems_TbItemTypes");

            entity.HasOne(d => d.Os).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.OsId)
                .HasConstraintName("FK_TbItems_TbOs");

            entity.HasOne(d => d.Processor).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.ProcessorId)
                .HasConstraintName("FK_TbItems_TbProcessors");

            entity.HasOne(d => d.Ram).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.RamId)
                .HasConstraintName("FK_TbItems_TbRAMs");

            entity.HasOne(d => d.ScreenResolution).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.ScreenResolutionId)
                .HasConstraintName("FK_TbItems_ScreenResolutions");

        
        });

    
        modelBuilder.Entity<TbItemImage>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.Property(e => e.ImageName).HasMaxLength(200);

            entity.HasOne(d => d.Item).WithMany(p => p.TbItemImages)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbItemImages_TbItems");
        });

        modelBuilder.Entity<TbItemType>(entity =>
        {
            entity.HasKey(e => e.ItemTypeId);

            entity.Property(e => e.CreatedBy).HasMaxLength(200);
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.ItemTypeName).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(200);
        });

        modelBuilder.Entity<TbO>(entity =>
        {
            entity.HasKey(e => e.OsId);

            entity.Property(e => e.CreatedBy).HasMaxLength(200);
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.OsName).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(200);
        });

        modelBuilder.Entity<TbProcessor>(entity =>
        {
            entity.HasKey(e => e.ProcessorId);

            entity.Property(e => e.ProcessorName)
                .HasMaxLength(250)
                .IsUnicode(false);
        });


        modelBuilder.Entity<TbRam>(entity =>
        {
            entity.HasKey(e => e.RamId);

            entity.ToTable("TbRAMs");

            entity.Property(e => e.RamId).HasColumnName("RAMId");
            entity.Property(e => e.RamSize).HasColumnName("RAMSize");
        });

        modelBuilder.Entity<TbSalesInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValue("");
            entity.Property(e => e.DelivryDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(200);
        });

        modelBuilder.Entity<TbSalesInvoiceItem>(entity =>
        {
            entity.HasKey(e => e.InvoiceItemId);

            entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.Qty).HasDefaultValue(1.0);

            entity.HasOne(d => d.Invoice).WithMany(p => p.TbSalesInvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSalesInvoiceItems_TbSalesInvoices");

            entity.HasOne(d => d.Item).WithMany(p => p.TbSalesInvoiceItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSalesInvoiceItems_TbItems");
        });

        modelBuilder.Entity<TbScreenResolution>(entity =>
        {
            entity.HasKey(e => e.ScreenResolutionId);

            entity.Property(e => e.ScreenResolutionName)
                .HasMaxLength(250)
                .IsUnicode(false);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
