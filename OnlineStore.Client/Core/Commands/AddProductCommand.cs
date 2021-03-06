﻿namespace OnlineStore.Client.Core.Commands
{
    using Data;
    using Models;
    using Utilities;
    using System;
    using System.Linq;

    public class AddProductCommand
    {
        public string Execute()
        {
            if (!Authorization.Instance.ValidateIsUserLoggedIn())

            {
                throw new InvalidOperationException("You should log in first for this operation!");
            }

            if (Authorization.Instance.CurrentUser.Role != UserRole.Admin)
            {
                throw new InvalidOperationException("You don't have access to this operation!");
            }
            string result = string.Empty;
            Console.Write("Enter product name: ");
            string productName = Console.ReadLine().ToLower();

            using (var context = new OnlineStoreContext())
            {
                var productByName = context.Products.FirstOrDefault(p => p.Name.ToLower() == productName);
                if (productByName != null)
                {
                    throw new Exception($"Product {productName} already exist in database!");
                }
                Console.Write("Enter product price: ");
                decimal productPrice = decimal.Parse(Console.ReadLine());
                Console.Clear();
                Console.Write("Enter product unit: ");
                string productUnit = Console.ReadLine();
                Console.Clear();
                Console.Write("Enter product delivery price: ");
                decimal deliveryPrice = decimal.Parse(Console.ReadLine());
                Console.Clear();
                Console.Write("Enter product supplier: ");
                string productSupplier = Console.ReadLine().ToLower();
                Console.Clear();
                var supplierByName = context.Suppliers.FirstOrDefault(s => s.Name.ToLower() == productSupplier);
                if (supplierByName == null)
                {
                    throw new Exception($"Supplier {productSupplier} does not exist in database! You must add supplier first!");
                }
                Product product = new Product()
                {
                    Name = productName,
                    Price = productPrice,
                    Unit = productUnit,
                    Quantity = 0,
                    Supplier = supplierByName,
                    DeliveryPrice = deliveryPrice
                };
                context.Products.Add(product);
                context.SaveChanges();

                result = $"{productName} was added succesfully to database";
            }
            return result;
        }

    }
}

