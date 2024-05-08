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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Blogs__3214EC067D01AB55")
                    .IsUnique();

                entity.Property(e => e.BlogName).HasMaxLength(255);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK__Blogs__SportId__47DBAE45");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Brands__3214EC0683C3559A")
                    .IsUnique();

                entity.Property(e => e.BrandName).HasMaxLength(255);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Brands__Category__2F10007B");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Carts__3214EC062D9B3E6F")
                    .IsUnique();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Carts__UserId__66603565");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__CartItem__3214EC065AB1FAF4")
                    .IsUnique();

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK__CartItems__CartI__6754599E");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__CartItems__Produ__6FE99F9F");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Categori__3214EC06AD4E4B7E")
                    .IsUnique();

                entity.Property(e => e.CategoryName).HasMaxLength(255);

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK__Categorie__Sport__2B3F6F97");
            });

            modelBuilder.Entity<ImagesVideo>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__ImagesVi__3214EC062D9B192C")
                    .IsUnique();

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.Video).IsRequired();

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.ImagesVideos)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ImagesVid__BlogI__656C112C");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ImagesVideos)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ImagesVid__Produ__6477ECF3");
            });

            modelBuilder.Entity<ImportHistory>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__ImportHi__3214EC060AA37703")
                    .IsUnique();

                entity.Property(e => e.ImportCode).HasMaxLength(255);

                entity.Property(e => e.ImportDate).HasColumnType("datetime");

                entity.Property(e => e.LotCode).HasMaxLength(255);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ImportHistories)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ImportHis__Produ__76969D2E");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ImportHistories)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__ImportHis__Suppl__778AC167");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Likes__3214EC06E1FEC2AA")
                    .IsUnique();

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK__Likes__BlogId__6C190EBB");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Likes__ProductId__6B24EA82");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Likes__UserId__6D0D32F4");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Orders__3214EC0607648FD6")
                    .IsUnique();

                entity.Property(e => e.IntoMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OrderCode).HasMaxLength(255);

                entity.Property(e => e.ReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TransportFee).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK__Orders__PaymentM__71D1E811");

                entity.HasOne(d => d.ShipmentDetail)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipmentDetailId)
                    .HasConstraintName("FK__Orders__Shipment__70DDC3D8");

                entity.HasOne(d => d.TransportUnit)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TransportUnitId)
                    .HasConstraintName("FK__Orders__Transpor__787EE5A0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__UserId__72C60C4A");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__OrderDet__3214EC06A3E14A12")
                    .IsUnique();

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__73BA3083");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderDeta__Produ__74AE54BC");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");

                entity.HasIndex(e => e.Id, "UQ__PaymentM__3214EC06CECDF531")
                    .IsUnique();

                entity.Property(e => e.PaymentMethodName).HasMaxLength(255);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Products__3214EC0654722359")
                    .IsUnique();

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ListedPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Offers).IsRequired();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductCode).HasMaxLength(255);

                entity.Property(e => e.ProductName).HasMaxLength(255);

                entity.Property(e => e.Size).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__Products__BrandI__6A30C649");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products__Catego__693CA210");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK__Products__SportI__32E0915F");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.HasIndex(e => e.RefreshTokenId, "UQ__RefreshT__F5845E38A02F052E")
                    .IsUnique();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RefreshTo__UserI__52593CB8");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Reviews__3214EC06F7AFC0F6")
                    .IsUnique();

                entity.Property(e => e.Review1)
                    .HasMaxLength(255)
                    .HasColumnName("Review");

                entity.Property(e => e.Star).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Reviews__Product__6E01572D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Reviews__UserId__6EF57B66");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Roles__3214EC060C8A4764")
                    .IsUnique();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.RoleName).HasMaxLength(255);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ShipmentDetail>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Shipment__3214EC068B064DE4")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShipmentDetails)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ShipmentD__UserI__68487DD7");
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Sports__3214EC06AF24A1C9")
                    .IsUnique();
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Supplier__3214EC062EFF7ECC")
                    .IsUnique();

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.SupplierName).HasMaxLength(255);
            });

            modelBuilder.Entity<TransportUnit>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Transpor__3214EC0652BA4620")
                    .IsUnique();

                entity.Property(e => e.TransportUnitName).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Users__3214EC0680E67A25")
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
                    .HasConstraintName("FK__Users__RoleId__4E88ABD4");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Warehous__3214EC069ADA7E98")
                    .IsUnique();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Warehouse__Produ__75A278F5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
