
namespace OnlineStore.Client.Core.Commands
{
    using OnlineStore.Client.Utilities;
    using OnlineStore.Data;
    using OnlineStore.Models;
    using System;
    using System.Linq;
    public class ListDeliveriesCommand
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
            Console.WriteLine("Tip: search parameters are: SpecificTimePeriod/Today/BySupplier/ByProduct/All");
            Console.Write("Enter search parameter: ");
            string param = Console.ReadLine().ToLower();

            switch (param)
            {
                case "specifictimeperiod":
                    Console.Clear();
                    Console.Write("Enter start date(dd/mm/yyyy):");
                    string start = Console.ReadLine();
                    Console.Write("Enter end date(dd/mm/yyyy):");
                    string end = Console.ReadLine();
                    DateTime startDate = DateTime.Parse(start);
                    DateTime endDate = DateTime.Parse(end);

                    using (OnlineStoreContext context = new OnlineStoreContext())
                    {
                        var deliveries = context.Deliveries.Where(d => d.DeliveryDate >= startDate && d.DeliveryDate <= endDate).ToList();
                        if (deliveries.Count==0)
                        {
                            result+="There are no deliveries in specific period";
                        }
                        else
                        {
                            result += "-------------------------------------";
                            foreach (var d in deliveries)
                            {
                                result += $"\nDate: {d.DeliveryDate.Day}/{d.DeliveryDate.Month}/{d.DeliveryDate.Year}";
                                result += $"\nSupplier: {d.Supplier.Name}";
                                result += "\nProducts:";
                                foreach (var p in d.Products)
                                {
                                    result += $"\n--name: {p.Product.Name}\n--quantity: {p.DeliveredQuantity} {p.Product.Unit}\n--price: {p.Product.DeliveryPrice}";
                                }
                                result += $"\nTotal price: {d.TotalSum:F2}";
                                result += "\n-------------------------------------";
                            }
                        }
                    }
                    break;

                case "today":
                    DateTime date = DateTime.Now.Date;

                    using (OnlineStoreContext context = new OnlineStoreContext())
                    {
                        var deliveries = context.Deliveries.Where(d => d.DeliveryDate == date).ToList();
                        if (deliveries.Count == 0)
                        {
                            result+="There are no deliveries today";
                        }
                        else
                        {
                            result += "-------------------------------------";
                            foreach (var d in deliveries)
                            {
                                result += $"\nDate: {d.DeliveryDate.Day}/{d.DeliveryDate.Month}/{d.DeliveryDate.Year}";
                                result += $"\nSupplier: {d.Supplier.Name}";
                                result += "\nProducts:";
                                foreach (var p in d.Products)
                                {
                                    result += $"\n--name: {p.Product.Name}\n--quantity: {p.DeliveredQuantity} {p.Product.Unit}\n--price: {p.Product.DeliveryPrice}";
                                }
                                result += $"\nTotal price: {d.TotalSum:F2}";
                                result += "\n-------------------------------------";
                            }
                        }
                    }
                    break;

                case "bysupplier":
                    Console.Clear();
                    Console.Write("Enter supplier name:");
                    string supplierName = Console.ReadLine().ToLower();

                    using (OnlineStoreContext context = new OnlineStoreContext())
                    {
                        if (context.Suppliers.FirstOrDefault(s => s.Name.ToLower() == supplierName) == null)
                        {
                            result+=$"Supplier {supplierName} does not exist in database!";
                        }
                        var deliveries = context.Deliveries.Where(d => d.Supplier.Name.ToLower() == supplierName).ToList();
                        if (deliveries.Count == 0)
                        {
                            result+=$"There are no deliveries by supplier {supplierName}";
                        }
                        else
                        {
                            result += "-------------------------------------";
                            foreach (var d in deliveries)
                            {
                                result += $"\nDate: {d.DeliveryDate.Day}/{d.DeliveryDate.Month}/{d.DeliveryDate.Year}";
                                result += $"\nSupplier: {d.Supplier.Name}";
                                result += "\nProducts:";
                                foreach (var p in d.Products)
                                {
                                    result += $"\n--name: {p.Product.Name}\n--quantity: {p.DeliveredQuantity} {p.Product.Unit}\n--price: {p.Product.DeliveryPrice}";
                                }
                                result += $"\nTotal price: {d.TotalSum:F2}";
                                result += "\n-------------------------------------";
                            }
                        }
                    }
                    break;

                case "byproduct":
                    Console.Clear();
                    Console.Write("Enter product name:");
                    string productName = Console.ReadLine().ToLower();

                    using (OnlineStoreContext context = new OnlineStoreContext())
                    {
                        if (context.Products.FirstOrDefault(p => p.Name.ToLower() == productName) == null)
                        {
                            result+=$"Product {productName} does not exist in database!";
                        }
                        var deliveries = context.ProductDeliveries.Where(d => d.Product.Name.ToLower() == productName).ToList();
                        if (deliveries.Count == 0)
                        {
                            result+=$"There are no deliveries by product {productName}";
                        }
                        else
                        {
                            result += "-------------------------------------";
                            foreach (var d in deliveries)
                            {
                                result += $"\nDate: {d.Delivery.DeliveryDate.Day}/{d.Delivery.DeliveryDate.Month}/{d.Delivery.DeliveryDate.Year}";
                                result += $"\nSupplier: {d.Delivery.Supplier.Name}";
                                result += "\nProducts:";
                                foreach (var p in d.Delivery.Products)
                                {
                                    result += $"\n--name: {p.Product.Name}\n--quantity: {p.DeliveredQuantity} {p.Product.Unit}\n--price: {p.Product.DeliveryPrice}";
                                }
                                result += $"\nTotal price: {d.Delivery.TotalSum:F2}";
                                result += "\n-------------------------------------";
                            }
                        }
                    }
                    break;

                case "all":
                    using (OnlineStoreContext context = new OnlineStoreContext())
                    {
                        var deliveries = context.Deliveries.ToList();
                        if (deliveries.Count == 0)
                        {
                            result+=$"There are no deliveries at all";
                        }
                        else
                        {
                            result += "-------------------------------------";
                            foreach (var d in deliveries)
                            {
                                result += $"\nDate: {d.DeliveryDate.Day}/{d.DeliveryDate.Month}/{d.DeliveryDate.Year}";
                                result += "\nProducts:";
                                foreach (var p in d.Products)
                                {
                                    result += $"\n--name: {p.Product.Name}\n--quantity: {p.DeliveredQuantity} {p.Product.Unit}\n--price: {p.Product.DeliveryPrice}";
                                }
                                result += $"\nTotal price: {d.TotalSum:F2}";
                                result += "\n-------------------------------------";
                            }
                        }
                    }
                    break;

                default:
                    throw new InvalidOperationException($"{param} is invalid!");
            }
            return result;
        }
    }
}
