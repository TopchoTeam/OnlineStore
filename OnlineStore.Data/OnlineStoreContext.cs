namespace OnlineStore.Data
{
    using Models;
    using System.Data.Entity;
    using Migrations;

    public class OnlineStoreContext : DbContext
    {
        public OnlineStoreContext()
    : base("name=OnlineStoreContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<OnlineStoreContext, Configuration>());
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<ProductSale> ProductSales { get; set; }

       protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<User>().HasOptional(u => u.Account).WithOptionalDependent(a => a.User).WillCascadeOnDelete(false);
            
            modelBuilder.Entity<User>().HasRequired(u => u.Account).WithRequiredDependent(a => a.User).WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }

}