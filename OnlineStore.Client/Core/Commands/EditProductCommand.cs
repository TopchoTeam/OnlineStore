namespace OnlineStore.Client.Core.Commands
{
    using Utilities;
    using Data;
    using Models;
    using System;
    using System.Linq;

    public class EditProductCommand
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
            Console.Clear();
            Console.Write("Enter product name: ");
            string productName = Console.ReadLine().ToLower();

            using (var context = new OnlineStoreContext())
            {
                var productByName = context.Products.FirstOrDefault(p => p.Name.ToLower() == productName);
                if (productByName == null)
                {
                    throw new Exception($"Product {productName} does not exist in database!");
                }
                Console.Clear();
                Console.WriteLine("Tip: you can change Price/Unit/Quantity/Supplier.");
                Console.Write("Enter product property: ");
               
                string productProperty = Console.ReadLine().ToLower();
                switch (productProperty)
                {
                    case "price":
                        Console.Clear();
                        Console.Write("Enter new product price: ");                        
                        decimal productPrice = decimal.Parse(Console.ReadLine());
                        productByName.Price = productPrice;                       
                        break;

                    case "unit":
                        Console.Clear();
                        Console.Write("Enter new product unit: ");                     
                        string productUnit = Console.ReadLine().ToLower();
                        productByName.Unit = productUnit;
                        break;
                    case "quantity":
                        Console.Clear();
                        Console.Write("Enter new product quantity: ");                       
                        int productQuantity = int.Parse(Console.ReadLine());
                        productByName.Quantity = productQuantity;
                        break;
                    case "supplier":
                        Console.Clear();
                        Console.Write("Enter new supplier: ");                        
                        string productSupplierName = Console.ReadLine();
                        var supplierByName = context.Suppliers.FirstOrDefault(s => s.Name == productSupplierName);
                        if (supplierByName == null)
                        {
                            throw new Exception($"Supplier {productSupplierName} does not exist in database!");
                        }
                        productByName.SupplierId = supplierByName.SupplierId;
                        break;
                    default:
                        break;
                }
                context.SaveChanges();
                result = $"Product {productProperty} was edited successfully!";
            }
            
            return result;
        }
    }
}
