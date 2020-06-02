using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NorthwindExample.Models
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext()
        {
        }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlphabeticalListOfProducts> AlphabeticalListOfProducts { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CategorySalesFor1997> CategorySalesFor1997 { get; set; }
        public virtual DbSet<CurrentProductList> CurrentProductList { get; set; }
        public virtual DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCity { get; set; }
        public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
        public virtual DbSet<CustomerDemographics> CustomerDemographics { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<EmployeeTerritories> EmployeeTerritories { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<OrderDetailsExtended> OrderDetailsExtended { get; set; }
        public virtual DbSet<OrderSubtotals> OrderSubtotals { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrdersQry> OrdersQry { get; set; }
        public virtual DbSet<ProductSalesFor1997> ProductSalesFor1997 { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrice { get; set; }
        public virtual DbSet<ProductsByCategory> ProductsByCategory { get; set; }
        public virtual DbSet<QuarterlyOrders> QuarterlyOrders { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<SalesByCategory> SalesByCategory { get; set; }
        public virtual DbSet<SalesTotalsByAmount> SalesTotalsByAmount { get; set; }
        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarter { get; set; }
        public virtual DbSet<SummaryOfSalesByYear> SummaryOfSalesByYear { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Territories> Territories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=Northwind;User ID=sa;Password=<YourStrong!Passw0rd>;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlphabeticalListOfProducts>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Alphabetical list of products");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.HasIndex(e => e.CategoryName)
                    .HasName("CategoryName");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Picture).HasColumnType("image");
            });

            modelBuilder.Entity<CategorySalesFor1997>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Category Sales for 1997");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CategorySales).HasColumnType("money");
            });

            modelBuilder.Entity<CurrentProductList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Current Product List");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<CustomerAndSuppliersByCity>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Customer and Suppliers by City");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.Relationship)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerCustomerDemo>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.CustomerTypeId })
                    .IsClustered(false);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.CustomerTypeId)
                    .HasColumnName("CustomerTypeID")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerCustomerDemo)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCustomerDemo_Customers");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.CustomerCustomerDemo)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCustomerDemo");
            });

            modelBuilder.Entity<CustomerDemographics>(entity =>
            {
                entity.HasKey(e => e.CustomerTypeId)
                    .IsClustered(false);

                entity.Property(e => e.CustomerTypeId)
                    .HasColumnName("CustomerTypeID")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CustomerDesc).HasColumnType("ntext");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.HasIndex(e => e.City)
                    .HasName("City");

                entity.HasIndex(e => e.CompanyName)
                    .HasName("CompanyName");

                entity.HasIndex(e => e.PostalCode)
                    .HasName("PostalCode");

                entity.HasIndex(e => e.Region)
                    .HasName("Region");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);
            });

            modelBuilder.Entity<EmployeeTerritories>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.TerritoryId })
                    .IsClustered(false);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.TerritoryId)
                    .HasColumnName("TerritoryID")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeTerritories)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTerritories_Employees");

                entity.HasOne(d => d.Territory)
                    .WithMany(p => p.EmployeeTerritories)
                    .HasForeignKey(d => d.TerritoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTerritories_Territories");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.HasIndex(e => e.LastName)
                    .HasName("LastName");

                entity.HasIndex(e => e.PostalCode)
                    .HasName("PostalCode");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Extension).HasMaxLength(4);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.HomePhone).HasMaxLength(24);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Notes).HasColumnType("ntext");

                entity.Property(e => e.Photo).HasColumnType("image");

                entity.Property(e => e.PhotoPath).HasMaxLength(255);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.Title).HasMaxLength(30);

                entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);

                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("FK_Employees_Employees");
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Invoices");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ExtendedPrice).HasColumnType("money");

                entity.Property(e => e.Freight).HasColumnType("money");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.Salesperson)
                    .IsRequired()
                    .HasMaxLength(31);

                entity.Property(e => e.ShipAddress).HasMaxLength(60);

                entity.Property(e => e.ShipCity).HasMaxLength(15);

                entity.Property(e => e.ShipCountry).HasMaxLength(15);

                entity.Property(e => e.ShipName).HasMaxLength(40);

                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);

                entity.Property(e => e.ShipRegion).HasMaxLength(15);

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.Property(e => e.ShipperName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK_Order_Details");

                entity.ToTable("Order Details");

                entity.HasIndex(e => e.OrderId)
                    .HasName("OrdersOrder_Details");

                entity.HasIndex(e => e.ProductId)
                    .HasName("ProductsOrder_Details");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Products");
            });

            modelBuilder.Entity<OrderDetailsExtended>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Order Details Extended");

                entity.Property(e => e.ExtendedPrice).HasColumnType("money");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<OrderSubtotals>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Order Subtotals");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Subtotal).HasColumnType("money");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.HasIndex(e => e.CustomerId)
                    .HasName("CustomersOrders");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("EmployeesOrders");

                entity.HasIndex(e => e.OrderDate)
                    .HasName("OrderDate");

                entity.HasIndex(e => e.ShipPostalCode)
                    .HasName("ShipPostalCode");

                entity.HasIndex(e => e.ShipVia)
                    .HasName("ShippersOrders");

                entity.HasIndex(e => e.ShippedDate)
                    .HasName("ShippedDate");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.ShipAddress).HasMaxLength(60);

                entity.Property(e => e.ShipCity).HasMaxLength(15);

                entity.Property(e => e.ShipCountry).HasMaxLength(15);

                entity.Property(e => e.ShipName).HasMaxLength(40);

                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);

                entity.Property(e => e.ShipRegion).HasMaxLength(15);

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.ShipViaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipVia)
                    .HasConstraintName("FK_Orders_Shippers");
            });

            modelBuilder.Entity<OrdersQry>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Orders Qry");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Freight).HasColumnType("money");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.ShipAddress).HasMaxLength(60);

                entity.Property(e => e.ShipCity).HasMaxLength(15);

                entity.Property(e => e.ShipCountry).HasMaxLength(15);

                entity.Property(e => e.ShipName).HasMaxLength(40);

                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);

                entity.Property(e => e.ShipRegion).HasMaxLength(15);

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductSalesFor1997>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Product Sales for 1997");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ProductSales).HasColumnType("money");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.HasIndex(e => e.CategoryId)
                    .HasName("CategoryID");

                entity.HasIndex(e => e.ProductName)
                    .HasName("ProductName");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("SuppliersProducts");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

                entity.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Products_Suppliers");
            });

            modelBuilder.Entity<ProductsAboveAveragePrice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Products Above Average Price");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<ProductsByCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Products by Category");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);
            });

            modelBuilder.Entity<QuarterlyOrders>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Quarterly Orders");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName).HasMaxLength(40);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .IsClustered(false);

                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RegionDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<SalesByCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Sales by Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ProductSales).HasColumnType("money");
            });

            modelBuilder.Entity<SalesTotalsByAmount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Sales Totals by Amount");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.SaleAmount).HasColumnType("money");

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Shippers>(entity =>
            {
                entity.HasKey(e => e.ShipperId);

                entity.Property(e => e.ShipperId).HasColumnName("ShipperID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Phone).HasMaxLength(24);
            });

            modelBuilder.Entity<SummaryOfSalesByQuarter>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Summary of Sales by Quarter");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.Property(e => e.Subtotal).HasColumnType("money");
            });

            modelBuilder.Entity<SummaryOfSalesByYear>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Summary of Sales by Year");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.Property(e => e.Subtotal).HasColumnType("money");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.HasKey(e => e.SupplierId);

                entity.HasIndex(e => e.CompanyName)
                    .HasName("CompanyName");

                entity.HasIndex(e => e.PostalCode)
                    .HasName("PostalCode");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.HomePage).HasColumnType("ntext");

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);
            });

            modelBuilder.Entity<Territories>(entity =>
            {
                entity.HasKey(e => e.TerritoryId)
                    .IsClustered(false);

                entity.Property(e => e.TerritoryId)
                    .HasColumnName("TerritoryID")
                    .HasMaxLength(20);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.TerritoryDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Territories)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Territories_Region");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
