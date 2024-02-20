using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoodServeAPI.Models
{
    public partial class FoodServeContext : DbContext
    {
        public FoodServeContext()
        {
        }

        public FoodServeContext(DbContextOptions<FoodServeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FsFood> FsFood { get; set; }
        public virtual DbSet<FsFoodOrder> FsFoodOrder { get; set; }
        public virtual DbSet<FsOrderDetail> FsOrderDetail { get; set; }
        public virtual DbSet<FsRole> FsRole { get; set; }
        public virtual DbSet<FsUser> FsUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;uid=sa;pwd=sqlserver1;Database=FoodServe;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FsFood>(entity =>
            {
                entity.HasKey(e => e.FoodId);

                entity.ToTable("FS_Food");

                entity.Property(e => e.FoodId)
                    .HasColumnName("food_id")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ActiveDate)
                    .HasColumnName("active_date")
                    .HasColumnType("date");

                entity.Property(e => e.FoodName)
                    .IsRequired()
                    .HasColumnName("food_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FoodPrice)
                    .HasColumnName("food_price")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FsFoodOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("FS_Food_Order");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.IsClose)
                    .IsRequired()
                    .HasColumnName("is_close")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PriceTotal)
                    .HasColumnName("price_total")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.QtyTotal)
                    .HasColumnName("qty_total")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FsFoodOrder)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FS_Food_Order_FS_User");
            });

            modelBuilder.Entity<FsOrderDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId);

                entity.ToTable("FS_Order_Detail");

                entity.Property(e => e.DetailId)
                    .HasColumnName("detail_id")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.FoodId)
                    .IsRequired()
                    .HasColumnName("food_id")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasColumnName("order_id")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Qty)
                    .HasColumnName("qty")
                    .HasColumnType("numeric(3, 0)");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FsOrderDetail)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FS_Order_Detail_FS_Food");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.FsOrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FS_Order_Detail_FS_Food_Order");
            });

            modelBuilder.Entity<FsRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("FS_Role");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ActiveDate).HasColumnType("date");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
                
            });

            modelBuilder.Entity<FsUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("FS_User");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ActiveDate).HasColumnType("date");

                entity.Property(e => e.DoB).HasColumnType("date");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.FsUser)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FS_User_FS_Role");
            });
        }
    }
}
