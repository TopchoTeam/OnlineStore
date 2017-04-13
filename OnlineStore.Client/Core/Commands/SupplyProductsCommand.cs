using OnlineStore.Client.Utilities;
using OnlineStore.Data;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Client.Core.Commands
{
    public class SupplyProductsCommand
    {
        public string Execute()
        {
            string result = string.Empty;

            if (!Authorization.Instance.ValidateIsUserLoggedIn())
            {
                throw new InvalidOperationException("You should log in first for this operation");
            }
            if (Authorization.Instance.CurrentUser.Role != UserRole.Admin)
            {
                throw new InvalidOperationException("You don't have access to this operation!");
            }

            Console.Clear();
            Console.Write("Enter supplier name: ");
            string supplierName = Console.ReadLine().ToLower();
            using (OnlineStoreContext context = new OnlineStoreContext())
            {
                Supplier supplier = context.Suppliers.FirstOrDefault(s => s.Name == supplierName);
                if (supplier == null)
                {
                    throw new Exception($"Supplier {supplierName} does not exist in database!");
                }
                Delivery delivery = new Delivery();
                delivery.SupplierId = supplier.SupplierId;
                delivery.DeliveryDate = DateTime.Now.Date;
                           
                Console.Clear();
                Console.WriteLine("Tip: If you want to stop adding products type \"Stop\"");
                Console.Write("Enter product name: ");
                string productName = Console.ReadLine().ToLower();
                while (productName != "stop")
                {
                    Product product = context.Products.FirstOrDefault(p => p.Name.ToLower() == productName);

                    if (product == null)
                    {
                        Console.WriteLine($"Product {productName} does not exist in database!");
                        Console.Write("Enter product name: ");
                    }
                    else
                    {
                        Console.Clear();
                        Console.Write("Enter product delivery price: ");
                        decimal deliveryPrice = decimal.Parse(Console.ReadLine());
                        product.DeliveryPrice = deliveryPrice;

                        Console.Clear();
                        Console.Write("Enter quantity: ");
                        int quantity = int.Parse(Console.ReadLine());
                        Console.Clear();
                        if (delivery.Products.Any(p => p.Product.ProductId == product.ProductId))
                        {
                            delivery.Products.FirstOrDefault(d => d.Product.ProductId == product.ProductId).DeliveredQuantity += quantity;
                            delivery.TotalSum += quantity * product.DeliveryPrice;
                        }
                        else
                        {
                            ProductDelivery proDelivery = new ProductDelivery()
                            {
                                Product = product,
                                DeliveredQuantity = quantity                                
                            };
                            delivery.Products.Add(proDelivery);
                            delivery.TotalSum += quantity * product.DeliveryPrice;
                            product.Quantity += quantity;
                        }
                    }
                    Console.WriteLine("Tip: If you want to stop adding products type \"Stop\"");
                    Console.Write("Enter product name: ");
                    productName = Console.ReadLine().ToLower();
                }

                Console.Clear();
                Console.WriteLine("Products you are buying:");
                foreach (var d in delivery.Products)
                {
                    Console.WriteLine($"-- name: {d.Product.Name}\n--quantity: {d.DeliveredQuantity}");
                    Console.WriteLine("------------------------");
                }
                Console.WriteLine($"Total price: {delivery.TotalSum:F2}");
                Console.Write("Confirm sale (Yes/No): ");
                string confirm = Console.ReadLine().ToLower();
                if (confirm == "yes")
                {
                    User admin = context.Users.FirstOrDefault(u => u.UserName == "Admin");
                    if (delivery.TotalSum > admin.Account.Balance)
                    {
                        throw new InvalidProgramException("Insufficient account balance.");
                    }
                    else
                    {
                        admin.Account.Balance -= delivery.TotalSum;
                        context.Deliveries.Add(delivery);
                        context.SaveChanges();
                        result = "Delivery successful.";
                    }
                }
                else
                {
                    result = "Delivery terminated.";
                }
            }
            return result;
        }
    }
}
