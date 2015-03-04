using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataModel
{
    public class DbModel : IdentityDbContext<Customer>
    {
        public DbModel() : base("name=DbModel")
        {
            Database.SetInitializer(new DatabaseContextInitializer());
        }

        public static DbModel Create()
        {
            return new DbModel();
        }

        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StocksTransferItem> StocksTransferItems { get; set; }
        public virtual DbSet<StocksTransfer> StocksTransfers { get; set; }
        public virtual DbSet<sysdiagram> Sysdiagrams { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.Customer_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.Order_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.Product_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.Product_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.StocksTransferItems)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.Product_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StocksTransfer>()
                .Property(e => e.Distance)
                .HasPrecision(8, 2);

            modelBuilder.Entity<StocksTransfer>()
                .HasMany(e => e.StocksTransferItems)
                .WithRequired(e => e.StocksTransfer)
                .HasForeignKey(e => e.StockTransfer_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.Warehouse)
                .HasForeignKey(e => e.Warehouse_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.StocksTransfers)
                .WithRequired(e => e.Warehouse)
                .HasForeignKey(e => e.Destination_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.StocksTransfers1)
                .WithRequired(e => e.Warehouse1)
                .HasForeignKey(e => e.Departure_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
