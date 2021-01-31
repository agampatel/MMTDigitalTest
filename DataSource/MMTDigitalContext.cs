using Microsoft.EntityFrameworkCore;
using MMT.CustomerOrder.Models;

#nullable disable

namespace MMT.CustomerOrder.DataSource
{
    public partial class MMTDigitalContext : DbContext
    {
        public MMTDigitalContext()
        {
        }

        public MMTDigitalContext(DbContextOptions<MMTDigitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> Orderitems { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDERS");

                entity.Property(e => e.OrderId).HasColumnName("ORDERID");

                entity.Property(e => e.ContainsGift).HasColumnName("CONTAINSGIFT");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(10)
                    .HasColumnName("CUSTOMERID");

                entity.Property(e => e.DeliveryExpected)
                    .HasColumnType("date")
                    .HasColumnName("DELIVERYEXPECTED");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("ORDERDATE");

                entity.Property(e => e.OrderSource)
                    .HasMaxLength(30)
                    .HasColumnName("ORDERSOURCE");

                entity.Property(e => e.ShippingMode)
                    .HasMaxLength(30)
                    .HasColumnName("SHIPPINGMODE");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("ORDERITEMS");

                entity.Property(e => e.OrderItemId).HasColumnName("ORDERITEMID");

                entity.Property(e => e.OrderId).HasColumnName("ORDERID");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("PRICE");

                entity.Property(e => e.ProductId).HasColumnName("PRODUCTID");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.Property(e => e.Returnable).HasColumnName("RETURNABLE");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__ORDERITEM__ORDER__3B75D760");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ORDERITEM__PRODU__3C69FB99");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCTS");

                entity.Property(e => e.ProductId).HasColumnName("PRODUCTID");

                entity.Property(e => e.Colour)
                    .HasMaxLength(20)
                    .HasColumnName("COLOUR");

                entity.Property(e => e.PackHeight)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("PACKHEIGHT");

                entity.Property(e => e.PackWeight)
                    .HasColumnType("decimal(8, 3)")
                    .HasColumnName("PACKWEIGHT");

                entity.Property(e => e.PackWidth)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("PACKWIDTH");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("PRODUCTNAME");

                entity.Property(e => e.Size)
                    .HasMaxLength(20)
                    .HasColumnName("SIZE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
