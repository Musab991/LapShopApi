using System;
using System.Collections.Generic;
using DomainLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BuisnessLibrary.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<TbBusinessInfo> TbBusinessInfos { get; set; }

    public virtual DbSet<TbCashTransacion> TbCashTransacions { get; set; }

    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbCustomer> TbCustomers { get; set; }

    public virtual DbSet<TbGpu> TbGpus { get; set; }

    public virtual DbSet<TbHardDisk> TbHardDisks { get; set; }

    public virtual DbSet<TbIndexViewSetting> TbIndexViewSettings { get; set; }

    public virtual DbSet<TbItem> TbItems { get; set; }

    public virtual DbSet<TbItemDiscount> TbItemDiscounts { get; set; }

    public virtual DbSet<TbItemImage> TbItemImages { get; set; }

    public virtual DbSet<TbItemType> TbItemTypes { get; set; }

    public virtual DbSet<TbO> TbOs { get; set; }

    public virtual DbSet<TbProcessor> TbProcessors { get; set; }

    public virtual DbSet<TbPurchaseInvoice> TbPurchaseInvoices { get; set; }

    public virtual DbSet<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; }

    public virtual DbSet<TbRam> TbRams { get; set; }

    public virtual DbSet<TbSalesInvoice> TbSalesInvoices { get; set; }

    public virtual DbSet<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; }

    public virtual DbSet<TbScreenResolution> TbScreenResolutions { get; set; }

    public virtual DbSet<TbSetting> TbSettings { get; set; }

    public virtual DbSet<TbSlider> TbSliders { get; set; }

    public virtual DbSet<TbSupplier> TbSuppliers { get; set; }

    public virtual DbSet<TbViewName> TbViewNames { get; set; }

    public virtual DbSet<TempImageUrl> TempImageUrls { get; set; }

    public virtual DbSet<VwItem> VwItems { get; set; }

    public virtual DbSet<VwItemCategory> VwItemCategories { get; set; }

    public virtual DbSet<VwItemsOutOfInvoice> VwItemsOutOfInvoices { get; set; }

    public virtual DbSet<VwSalesInvoice> VwSalesInvoices { get; set; }

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
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<TbBusinessInfo>(entity =>
        {
            entity.HasKey(e => e.BusinessInfoId);

            entity.ToTable("TbBusinessInfo");

            entity.HasIndex(e => e.CutomerId, "IX_TbBusinessInfo_CutomerId").IsUnique();

            entity.Property(e => e.Budget).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.BusinessCardNumber).HasMaxLength(20);
            entity.Property(e => e.OfficePhone).HasMaxLength(200);

            entity.HasOne(d => d.Cutomer).WithOne(p => p.TbBusinessInfo).HasForeignKey<TbBusinessInfo>(d => d.CutomerId);
        });

        modelBuilder.Entity<TbCashTransacion>(entity =>
        {
            entity.HasKey(e => e.CashTransactionId);

            entity.ToTable("TbCashTransacion");

            entity.Property(e => e.CashDate).HasColumnType("datetime");
            entity.Property(e => e.CashValue).HasColumnType("decimal(8, 2)");
        });

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

        modelBuilder.Entity<TbCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.Property(e => e.CustomerName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbGpu>(entity =>
        {
            entity.HasKey(e => e.Gpuid);

            entity.ToTable("TbGPUs");

            entity.Property(e => e.Gpuid).HasColumnName("GPUId");
            entity.Property(e => e.Gpuname)
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

        modelBuilder.Entity<TbIndexViewSetting>(entity =>
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(200);
            entity.Property(e => e.Productpara).HasMaxLength(200);
            entity.Property(e => e.Title1SectionTspace)
                .HasMaxLength(200)
                .HasColumnName("Title1SectionTSpace");
            entity.Property(e => e.TitleInner1).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(200);
        });

        modelBuilder.Entity<TbItem>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.HasIndex(e => e.Gpuid, "IX_TbItems_GPUId");

            entity.HasIndex(e => e.HardDiskId, "IX_TbItems_HardDiskId");

            entity.HasIndex(e => e.ItemTypeId, "IX_TbItems_ItemTypeId");

            entity.HasIndex(e => e.OsId, "IX_TbItems_OsId");

            entity.HasIndex(e => e.ProcessorId, "IX_TbItems_ProcessorId");

            entity.HasIndex(e => e.Ramid, "IX_TbItems_RAMId");

            entity.HasIndex(e => e.ScreenResolutionId, "IX_TbItems_ScreenResolutionId");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValue("");
            entity.Property(e => e.CreatedDate).HasDefaultValue(new DateOnly(2020, 9, 20));
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.Gpuid).HasColumnName("GPUId");
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.Ramid).HasColumnName("RAMId");
            entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.ScreenSize).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(200);
            entity.Property(e => e.Weight).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbItems_TbCategories");

            entity.HasOne(d => d.Gpu).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.Gpuid)
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
                .HasForeignKey(d => d.Ramid)
                .HasConstraintName("FK_TbItems_TbRAMs");

            entity.HasOne(d => d.ScreenResolution).WithMany(p => p.TbItems)
                .HasForeignKey(d => d.ScreenResolutionId)
                .HasConstraintName("FK_TbItems_ScreenResolutions");

            entity.HasMany(d => d.Customers).WithMany(p => p.Items)
                .UsingEntity<Dictionary<string, object>>(
                    "TbCustomerItem",
                    r => r.HasOne<TbCustomer>().WithMany().HasForeignKey("CustomerId"),
                    l => l.HasOne<TbItem>().WithMany().HasForeignKey("ItemId"),
                    j =>
                    {
                        j.HasKey("ItemId", "CustomerId");
                        j.ToTable("TbCustomerItems");
                        j.HasIndex(new[] { "CustomerId" }, "IX_TbCustomerItems_CustomerId");
                    });
        });

        modelBuilder.Entity<TbItemDiscount>(entity =>
        {
            entity.HasKey(e => e.ItemDiscountId);

            entity.ToTable("TbItemDiscount");

            entity.HasIndex(e => e.ItemId, "IX_TbItemDiscount_ItemId");

            entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Item).WithMany(p => p.TbItemDiscounts)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbItemDiscounts_TbItems");
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

        modelBuilder.Entity<TbPurchaseInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId);

            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(200);

            entity.HasOne(d => d.Supplier).WithMany(p => p.TbPurchaseInvoices)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPurchaseInvoices_TbSuppliers");
        });

        modelBuilder.Entity<TbPurchaseInvoiceItem>(entity =>
        {
            entity.HasKey(e => e.InvoiceItemId);

            entity.Property(e => e.InvoiceItemId).ValueGeneratedNever();
            entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.Qty).HasDefaultValue(1.0);

            entity.HasOne(d => d.Invoice).WithMany(p => p.TbPurchaseInvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPurchaseInvoiceItems_TbPurchaseInvoices");

            entity.HasOne(d => d.Item).WithMany(p => p.TbPurchaseInvoiceItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPurchaseInvoiceItems_TbItems");
        });

        modelBuilder.Entity<TbRam>(entity =>
        {
            entity.HasKey(e => e.Ramid);

            entity.ToTable("TbRAMs");

            entity.Property(e => e.Ramid).HasColumnName("RAMId");
            entity.Property(e => e.Ramsize).HasColumnName("RAMSize");
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

        modelBuilder.Entity<TbSetting>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasDefaultValue("");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(200)
                .HasDefaultValue("");
            entity.Property(e => e.ContantEmail)
                .HasMaxLength(200)
                .HasDefaultValue("");
            entity.Property(e => e.FacebookLink).HasMaxLength(400);
            entity.Property(e => e.InstgramLink).HasMaxLength(400);
            entity.Property(e => e.LastPanner)
                .HasMaxLength(200)
                .HasDefaultValue("");
            entity.Property(e => e.Logo).HasMaxLength(400);
            entity.Property(e => e.MiddlePanner).HasMaxLength(400);
            entity.Property(e => e.TwitterLink).HasMaxLength(400);
            entity.Property(e => e.WebsiteDescription).HasMaxLength(400);
            entity.Property(e => e.WebsiteName).HasMaxLength(200);
            entity.Property(e => e.YoutubeLink).HasMaxLength(200);
        });

        modelBuilder.Entity<TbSlider>(entity =>
        {
            entity.HasKey(e => e.SliderId);

            entity.ToTable("TbSlider");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValue("");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(200);
        });

        modelBuilder.Entity<TbSupplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK_TbSupplier");

            entity.Property(e => e.SupplierName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbViewName>(entity =>
        {
            entity.Property(e => e.ControllerName).HasMaxLength(200);
            entity.Property(e => e.FormName).HasMaxLength(200);
        });

        modelBuilder.Entity<TempImageUrl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TempImag__3214EC07384E5E54");

            entity.Property(e => e.ImageUrl).HasMaxLength(500);
        });

        modelBuilder.Entity<VwItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwItems");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Gpuid).HasColumnName("GPUId");
            entity.Property(e => e.Gpuname)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("GPUName");
            entity.Property(e => e.HardDisk)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.ItemTypeName).HasMaxLength(100);
            entity.Property(e => e.OsName).HasMaxLength(100);
            entity.Property(e => e.Processor)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.Ramid).HasColumnName("RAMId");
            entity.Property(e => e.Ramsize).HasColumnName("RAMSize");
            entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.ScreenResolution)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwItemCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwItemCategories");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<VwItemsOutOfInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwItemsOutOfInvoices");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<VwSalesInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwSalesInvoices");

            entity.Property(e => e.DelivryDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
