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
                        ProductSale proSale = new ProductSale();
                        proSale.ProductId = product.ProductId;
                        proSale.OrderedQuantity = quantity;
                        sale.Products.Add(proSale);
                        sale.TotalSum += quantity * product.Price;
                        product.Quantity = product.Quantity - quantity;
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
                    result = "Sale terminated.";
                }
            }

            return result;
        }
    }
}
