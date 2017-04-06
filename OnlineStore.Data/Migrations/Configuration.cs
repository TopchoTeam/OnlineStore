namespace OnlineStore.Data.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineStore.Data.OnlineStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OnlineStore.Data.OnlineStoreContext context)
        {
          

           Account accountAdmin = new Account()
            {
                AccountNumber = "FICB56719534",
                Balance = 0
            };
            Account account2 = new Account()
            {
                AccountNumber = "RZBBBG19954567",
                Balance = 120
            };
            Account account3 = new Account()
            {
                AccountNumber = "RZBB94574599",
                Balance = 50
            };
            Account account4 = new Account()
            {
                AccountNumber = "UCBB12486219",
                Balance = 5
            };
            Account account5 = new Account()
            {
                AccountNumber = "UCBB89457632",
                Balance = 65
            };
            context.Accounts.AddOrUpdate(a => a.AccountNumber,accountAdmin);
            context.Accounts.AddOrUpdate(a => a.AccountNumber, account2);
            context.Accounts.AddOrUpdate(a => a.AccountNumber, account3);
            context.Accounts.AddOrUpdate(a => a.AccountNumber, account4);
            context.Accounts.AddOrUpdate(a => a.AccountNumber, account5);

            context.SaveChanges();

            User admin = new User()
            {
                UserId =1,
                UserName = "Admin",
                FirstName = "Admin",
                LastName = "Adminov",
                Email = "admin@abv.bg",
                Password = "admin123",
                Address = "Tintyava str.15-17",
                Role = UserRole.Admin,
                IsDeleted = false,
                Account = accountAdmin
            };
            
            User pesho = new User()
            {
                UserName = "pesho",
                FirstName = "Petar",
                LastName = "Petrov",
                Email = "pesho@abv.bg",
                Password = "pesho123",
                Address = "Bulgaria blv. 179",
                Role = UserRole.Customer,
                IsDeleted = false,
                Account = account2
            };
            User gosho = new User()
            {
                UserName = "gesh",
                FirstName = "Georgi",
                LastName = "Georgiev",
                Email = "gesh@yahoo.com",
                Password = "gesh123",
                Address = "Tzar Boris III blv. 18",
                Role = UserRole.Customer,
                IsDeleted = false,
                Account = account3
            };
            User anita = new User()
            {
                UserName = "Anita",
                FirstName = "Anna",
                LastName = "Apostolova",
                Email = "ani@yahoo.com",
                Password = "456ani",
                Address = "Sofiiski geroi bl.117 ap.34",
                Role = UserRole.Customer,
                IsDeleted = false,
                Account = account4
            };
            User Maria = new User()
            {
                UserName = "Mara",
                FirstName = "Maria",
                LastName = "Vasileva",
                Email = "mv@gmail.com",
                Password = "MVasi",
                Address = "Urvich bl.120 ap.48",
                Role = UserRole.Customer,
                IsDeleted = false,
                Account= account5
            };
            context.Users.AddOrUpdate(u => u.UserName, admin);
            context.SaveChanges();

            context.Users.AddOrUpdate(u => u.UserName, gosho);
            context.SaveChanges();

            context.Users.AddOrUpdate(u => u.UserName, pesho);
            context.SaveChanges();

            context.Users.AddOrUpdate(u => u.UserName, anita);
            context.SaveChanges();

            context.Users.AddOrUpdate(u => u.UserName, Maria);
            context.SaveChanges();

            Supplier ivanET = new Supplier()
            {
                Name = "ET Ivan Ivanov"
            };
            Supplier tandem = new Supplier()
            {
                Name = "Tandem Ltd"
            };
            Supplier simid = new Supplier()
            {
                Name = "Simid AD"
            };
            Supplier boni = new Supplier()
            {
                Name = "Boni Ltd"
            };
            Supplier madgarov = new Supplier()
            {
                Name = "Dimitar Madgarov Ltd"
            };
            context.Suppliers.AddOrUpdate(s => s.Name, ivanET);
            context.SaveChanges();
            context.Suppliers.AddOrUpdate(s => s.Name, tandem);
            context.SaveChanges();
            context.Suppliers.AddOrUpdate(s => s.Name, boni);
            context.SaveChanges();
            context.Suppliers.AddOrUpdate(s => s.Name, simid);
            context.SaveChanges();
            context.Suppliers.AddOrUpdate(s => s.Name, madgarov);
            context.SaveChanges();

           Product breadSimid = new Product()
            {
                Name = "bread Simid",
                Price = 0.98m,
                Quantity = 5,
                Unit = "prs",
                SupplierId = simid.SupplierId
            };
            Product yellowCheeseBoni = new Product()
            {
                Name = "yellow Cheese Boni",
                Price = 11.20m,
                Quantity = 4,
                Unit = "kg",
                SupplierId = boni.SupplierId
            };
            Product beconMadgarov = new Product()
            {
                Name = "smoked becon Madgarov",
                Price = 16.70m,
                Quantity = 2,
                Unit = "kg",
                SupplierId = madgarov.SupplierId
            };
            Product whiteCheeseMadgarov = new Product()
            {
                Name = "white Cheese Madgarov",
                Price = 12.50m,
                Quantity = 14,
                Unit = "kg",
                SupplierId = madgarov.SupplierId
            };
            Product sausageTandem = new Product()
            {
                Name = "Macedonska sausage Tandem",
                Price = 6.90m,
                Quantity = 6,
                Unit = "kg",
                SupplierId = tandem.SupplierId
            };
            Product tomatoIvanov = new Product()
            {
                Name = "BullHeart tomato Ivanov",
                Price = 4.00m,
                Quantity = 12,
                Unit = "kg",
                SupplierId = ivanET.SupplierId
            };
            Product potatoIvanov = new Product()
            {
                Name = "potato Ivanov",
                Price = 0.60m,
                Quantity = 50,
                Unit = "kg",
                SupplierId = ivanET.SupplierId
            };
            context.Products.AddOrUpdate(p => p.Name, breadSimid);
            context.Products.AddOrUpdate(p => p.Name, sausageTandem);
            context.Products.AddOrUpdate(p => p.Name, yellowCheeseBoni);
            context.Products.AddOrUpdate(p => p.Name, beconMadgarov);
            context.Products.AddOrUpdate(p => p.Name, whiteCheeseMadgarov);
            context.Products.AddOrUpdate(p => p.Name, tomatoIvanov);
            context.Products.AddOrUpdate(p => p.Name, potatoIvanov);

            context.SaveChanges();
        }
    }
}
