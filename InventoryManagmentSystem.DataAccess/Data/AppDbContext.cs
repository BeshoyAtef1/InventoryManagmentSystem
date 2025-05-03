using InventoryManagmentSystem.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> Options):base (Options) { }

        public DbSet<Product> products { get; set; }
        public DbSet<archiveTransactionRepo> ArchiveTransactions { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductWarehouse>()
                .HasKey(pw => new { pw.ProductId, pw.WarehouseId });

            // InventoryTransaction - DestinationWarehouse
            modelBuilder.Entity<InventoryTransaction>()
                .HasOne(t => t.DestinationWarehouse)
                .WithMany(w => w.DestinationTransactions)
                .HasForeignKey(t => t.DestinationWarehouseId);

            // InventoryTransaction - SourceWarehouse
            modelBuilder.Entity<InventoryTransaction>()
                .HasOne(t => t.SourceWarehouse)
                .WithMany(w => w.SourceTransactions)
                .HasForeignKey(t => t.SourceWarehouseId);
        }

    }
}
