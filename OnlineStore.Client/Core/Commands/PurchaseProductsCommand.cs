namespace OnlineStore.Client.Core.Commands
{
    using System;
    using Utilities;
    using Data;
    using Models;
    using System.Linq;
    using System.Collections.Generic;

    public class PurchaseProductsCommand
    {
        public string Execute()
        {
            string result = string.Empty;

            if (!Authorization.Instance.ValidateIsUserLoggedIn())
            {
                throw new InvalidOperationException("You should log in first for this operation");
            }
            using (OnlineStoreContext context = new OnlineStoreContext())
            {
                User user = context.Users.FirstOrDefault(u => u.UserName == Authorization.Instance.CurrentUser.UserName);
                Sale sale = new Sale();
                sale.OrderDate = DateTime.Now;
                sale.IsDelivered = false;
                sale.UserId = user.UserId;
                Dictionary<string, int> original = new Dictionary<string, int>();
                Console.Clear();
                Console.WriteLine("Tip: If you want to stop adding products type \"Stop\"");
                Console.Write("Enter product name: ");
                string productName = Console.ReadLine().ToLower();
                while (productName != "stop")
                {
                    Console.Clear();
                    Console.Write("Enter quantity: ");
                    int quantity = int.Parse(Console.ReadLine());
                    if (context.Products.Any(p => p.Name.ToLower() == productName && p.Quantity >= quantity))
                    {
                        Product product = context.Products.FirstOrDefault(p => p.Name.ToLower() == productName);
                        if (sale.Products.Any(p => p.ProductId == product.ProductId))
                        {
                            sale.Products.FirstOrDefault(p => p.ProductId == product.ProductId).OrderedQuantity += quantity;
                            sale.TotalSum += quantity * product.Price;
                        }
                        else
                        {
                            ProductSale proSale = new ProductSale();
                            proSale.ProductId = product.ProductId;
                            proSale.OrderedQuantity = quantity;
                            sale.Products.Add(proSale);
                            sale.TotalSum += quantity * product.Price;
                            original.Add(product.Name, product.Quantity);
                        }
                        ChangeProductQuantity(productName, quantity);
                    }
                    else
                    {
                        Console.WriteLine("Either product does not exist or product quantity is not enough");
                    }
                    Console.WriteLine("Tip: If you want to stop adding products type \"Stop\"");
                    Console.Write("Enter product name: ");
                    productName = Console.ReadLine().ToLower();
                }
                Console.Clear();
                Console.Write("Confirm sale (Yes/No): ");
                string confirm = Console.ReadLine().ToLower();
                if (confirm == "yes")
                {
                    if (sale.TotalSum > user.Account.Balance)
                    {
                        RestoreProductOriginalQuantity(original);
                        throw new InvalidProgramException("Insufficient account balance.");
                    }
                    User admin = context.Users.FirstOrDefault(u => u.UserName == "Admin");
                    user.Account.Balance -= sale.TotalSum;
                    admin.Account.Balance += sale.TotalSum;
                    context.Sales.Add(sale);
                    context.SaveChanges();
                    result = "Sale successful.";
                }
                else
                {
                    RestoreProductOriginalQuantity(original);
                    result = "Sale terminated.";
                }
            }

            return result;
        }

        private void RestoreProductOriginalQuantity(Dictionary<string, int> original)
        {
            using(OnlineStoreContext context = new OnlineStoreContext())
            {
                foreach (var p in original)
                {
                    Product pr = context.Products.FirstOrDefault(e => e.Name == p.Key);
                    pr.Quantity = p.Value;
                }
                context.SaveChanges();
            }
        }

        private void ChangeProductQuantity(string productName, int quantity)
        {
            using(OnlineStoreContext context = new OnlineStoreContext())
            {
                Product pro = context.Products.FirstOrDefault(p => p.Name.ToLower() == productName);
                pro.Quantity -= quantity;
                context.SaveChanges();
            }
        }
    }
}
