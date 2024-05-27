using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace _2Sport_BE.Repository.Models
{
    public partial class TwoSportDBContext : DbContext
    {
        public TwoSportDBContext()
        {
        }

        public TwoSportDBContext(DbContextOptions<TwoSportDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<BrandCategory> BrandCategories { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ImagesVideo> ImagesVideos { get; set; }
        public virtual DbSet<ImportHistory> ImportHistories { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ShipmentDetail> ShipmentDetails { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<TransportUnit> TransportUnits { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Blogs__3214EC06DC5DB814")
                    .IsUnique();

                entity.Property(e => e.BlogName).HasMaxLength(255);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.MainImageName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MainImagePath).IsUnicode(false);

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK__Blogs__SportId__17036CC0");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Brands__3214EC067A2AF853")
                    .IsUnique();

                entity.Property(e => e.BrandName).HasMaxLength(255);
            });

            modelBuilder.Entity<BrandCategory>(entity =>
            {
                entity.ToTable("BrandCategory");

                entity.HasIndex(e => e.Id, "UQ__BrandCat__3214EC064F461A31")
                    .IsUnique();

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.BrandCategories)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__BrandCate__Brand__45F365D3");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.BrandCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__BrandCate__Categ__44FF419A");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Carts__3214EC06693AF8CA")
                    .IsUnique();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Carts__UserId__245D67DE");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__CartItem__3214EC06BD51B437")
                    .IsUnique();

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK__CartItems__CartI__25518C17");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__CartItems__Produ__06CD04F7");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Categori__3214EC06990DC2F6")
                    .IsUnique();

                entity.Property(e => e.CategoryName).HasMaxLength(255);

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK__Categorie__Sport__3E52440B");
            });

            modelBuilder.Entity<ImagesVideo>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__ImagesVi__3214EC06E83620DC")
                    .IsUnique();

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.VideoName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.VideoPath)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.ImagesVideos)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK__ImagesVid__BlogI__236943A5");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ImagesVideos)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ImagesVid__Produ__22751F6C");
            });

            modelBuilder.Entity<ImportHistory>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__ImportHi__3214EC06DE224E00")
                    .IsUnique();

                entity.Property(e => e.ImportCode).HasMaxLength(255);

                entity.Property(e => e.ImportDate).HasColumnType("datetime");

                entity.Property(e => e.LotCode).HasMaxLength(255);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ImportHistories)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ImportHis__Produ__0D7A0286");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ImportHistories)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__ImportHis__Suppl__0E6E26BF");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Likes__3214EC06F7AC180A")
                    .IsUnique();

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK__Likes__BlogId__02FC7413");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Likes__ProductId__02084FDA");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Likes__UserId__03F0984C");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Orders__3214EC06FABAD9A4")
                    .IsUnique();

                entity.Property(e => e.IntoMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OrderCode).HasMaxLength(255);

                entity.Property(e => e.ReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TransportFee).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK__Orders__PaymentM__08B54D69");

                entity.HasOne(d => d.ShipmentDetail)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipmentDetailId)
                    .HasConstraintName("FK__Orders__Shipment__07C12930");

                entity.HasOne(d => d.TransportUnit)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TransportUnitId)
                    .HasConstraintName("FK__Orders__Transpor__0F624AF8");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__UserId__09A971A2");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__OrderDet__3214EC06B75CA833")
                    .IsUnique();

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__0A9D95DB");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderDeta__Produ__0B91BA14");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");

                entity.HasIndex(e => e.Id, "UQ__PaymentM__3214EC06624600B0")
                    .IsUnique();

                entity.Property(e => e.PaymentMethodName).HasMaxLength(255);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Products__3214EC068D0222EC")
                    .IsUnique();

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ListedPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MainImageName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MainImagePath).IsUnicode(false);

                entity.Property(e => e.Offers).IsRequired();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductCode).HasMaxLength(255);

                entity.Property(e => e.ProductName).HasMaxLength(255);

                entity.Property(e => e.Size).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__Products__BrandI__01142BA1");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products__Catego__00200768");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK__Products__SportI__49C3F6B7");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.HasIndex(e => e.RefreshTokenId, "UQ__RefreshT__F5845E384BFCC797")
                    .IsUnique();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RefreshTo__UserI__1BC821DD");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Reviews__3214EC060E41610E")
                    .IsUnique();

                entity.Property(e => e.Review1)
                    .HasMaxLength(255)
                    .HasColumnName("Review");

                entity.Property(e => e.Star).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Reviews__Product__04E4BC85");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Reviews__UserId__05D8E0BE");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Roles__3214EC0650DBADA8")
                    .IsUnique();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.RoleName).HasMaxLength(255);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ShipmentDetail>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Shipment__3214EC064ED72E07")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShipmentDetails)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ShipmentD__UserI__2645B050");
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Sports__3214EC068A10FC8F")
                    .IsUnique();
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Supplier__3214EC065E76D8A9")
                    .IsUnique();

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.SupplierName).HasMaxLength(255);
            });

            modelBuilder.Entity<TransportUnit>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Transpor__3214EC0632131BB3")
                    .IsUnique();

                entity.Property(e => e.TransportUnitName).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Users__3214EC0654C0AD89")
                    .IsUnique();

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Salary).HasMaxLength(20);

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__19DFD96B");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Warehous__3214EC0689A78639")
                    .IsUnique();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Warehouse__Produ__0C85DE4D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
