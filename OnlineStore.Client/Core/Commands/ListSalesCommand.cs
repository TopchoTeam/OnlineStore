namespace OnlineStore.Client.Core.Commands
{
    using System;
    using Data;
    using Utilities;
    using Models;
    using System.Linq;

    public class ListSalesCommand
    {
        public string Execute()
        {
            if (!Authorization.Instance.ValidateIsUserLoggedIn())
            {
                throw new InvalidOperationException("You should log in first for this operation");
            }
            if (Authorization.Instance.CurrentUser.Role != UserRole.Admin)
            {
                throw new InvalidOperationException("You don't have access to this operation!");
            }

            string result = string.Empty;
            Console.Clear();
            Console.WriteLine("Tip: search parameters are: SpecificTimePeriod/Today/ByCustomer/ByProduct/All");
            Console.Write("Enter search parameter: ");
            string par = Console.ReadLine().ToLower();

            switch (par)
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
                        var sales = context.Sales.Where(s => s.OrderDate >= startDate && s.OrderDate <= endDate);

                        if (sales.Count() == 0)
                        {
                            result = $"There are no sales in the time period {start} - {end}!";
                        }
                        else
                        {
                            result += "-------------------------------------";
                            foreach (var s in sales)
                            {
                                result += $"\nDate: {s.OrderDate.Day}/{s.OrderDate.Month}/{s.OrderDate.Year}";
                                result += $"\nCustomer:\nUsername: {s.User.UserName}\nFull name: {s.User.FirstName} {s.User.LastName}";
                                result += "\nProducts:";
                                foreach (var p in s.Products)
                                {
                                    result += $"\n--name: {p.Product.Name}\n--quantity: {p.OrderedQuantity}\n--price: {p.Product.Price}";
                                }
                                result += $"\nTotal price: {s.TotalSum:F2}";
                                result += $"\nProfit: {s.Profit:F2}";
                                result += "\n-------------------------------------";
                            }
                        }
                    }
                    break;
                case "today":
                    DateTime date = DateTime.Now.Date;

                    using (OnlineStoreContext context = new OnlineStoreContext())
                    {
                        var sales = context.Sales.Where(s => s.OrderDate == date);

                        if (sales.Count() == 0)
                        {
                            result = "There are no sales today!";
                        }
                        else
                        {
                            result += "-------------------------------------";
                            foreach (var s in sales)
                            {
                                result += $"\nDate: {s.OrderDate.Day}/{s.OrderDate.Month}/{s.OrderDate.Year}";
                                result += $"\nCustomer:\nUsername: {s.User.UserName}\nFull name: {s.User.FirstName} {s.User.LastName}";
                                result += "\nProducts:";
                                foreach (var p in s.Products)
                                {
                                    result += $"\n--name: {p.Product.Name}\n--quantity: {p.OrderedQuantity}\n--price: {p.Product.Price}";
                                }
                                result += $"\nTotal price: {s.TotalSum:F2}";
                                result += $"\nProfit: {s.Profit:F2}";
                                result += "\n-------------------------------------";
                            }
                        }
                    }
                    break;
                case "all":
                    using (OnlineStoreContext context = new OnlineStoreContext())
                    {
                        var sales = context.Sales;

                        if (sales.Count() == 0)
                        {
                            result = "There are no sales!";
                        }
                        else
                        {
                            result += "-------------------------------------";
                            foreach (var s in sales)
                            {
                                result += $"\nDate: {s.OrderDate.Day}/{s.OrderDate.Month}/{s.OrderDate.Year}";
                                result += $"\nCustomer:\nUsername: {s.User.UserName}\nFull name: {s.User.FirstName} {s.User.LastName}";
                                result += "\nProducts:";
                                foreach (var p in s.Products)
                                {
                                    result += $"\n--name: {p.Product.Name}\n--quantity: {p.OrderedQuantity}\n--price: {p.Product.Price}";
                                }
                                result += $"\nTotal price: {s.TotalSum:F2}";
                                result += $"\nProfit: {s.Profit:F2}";
                                result += "\n-------------------------------------";
                            }
                        }
                    }
                    break;
                case "bycustomer":
                    Console.Clear();
                    Console.Write("Enter username: ");
                    string name = Console.ReadLine().ToLower();
                    using (OnlineStoreContext context = new OnlineStoreContext())
                    {
                        if (!context.Users.Any(u => u.UserName.ToLower() == name))
                        {
                            throw new InvalidOperationException($"{name} doesn't exist!");
                        }

                        var sales = context.Sales.Where(s => s.User.UserName.ToLower() == name);

                        if (sales.Count() == 0)
                        {
                            result = $"Customer {name} has no sales!";
                        }
                        else
                        {
                            result += "-------------------------------------";
                            foreach (var s in sales)
                            {
                                result += $"\nDate: {s.OrderDate.Day}/{s.OrderDate.Month}/{s.OrderDate.Year}";
                                result += $"\nCustomer:\nUsername: {s.User.UserName}\nFull name: {s.User.FirstName} {s.User.LastName}";
                                result += "\nProducts:";
                                foreach (var p in s.Products)
                                {
                                    result += $"\n--name: {p.Product.Name}\n--quantity: {p.OrderedQuantity}\n--price: {p.Product.Price}";
                                }
                                result += $"\nTotal price: {s.TotalSum:F2}";
                                result += $"\nProfit: {s.Profit:F2}";
                                result += "\n-------------------------------------";
                            }
                        }
                    }
                        break;
                case "byproduct":
                    Console.Clear();
                    Console.Write("Enter product name:");
                    string proName = Console.ReadLine().ToLower();
                    using(OnlineStoreContext context = new OnlineStoreContext())
                    {
                        if (!context.Products.Any(p => p.Name.ToLower() == proName))
                        {
                            throw new InvalidOperationException($"Product {proName} doesn't exist!");
                        }

                        var sales = context.Sales.Where(s => s.Products.Any(p => p.Product.Name.ToLower() == proName));

                        if (sales.Count() == 0)
                        {
                            result = "There are no sales with this product!";
                        }
                        else
                        {
                            result += "-------------------------------------";
                            foreach (var s in sales)
                            {
                                result += $"\nDate: {s.OrderDate.Day}/{s.OrderDate.Month}/{s.OrderDate.Year}";
                                result += $"\nCustomer:\nUsername: {s.User.UserName}\nFull name: {s.User.FirstName} {s.User.LastName}";
                                result += "\nProducts:";
                                foreach (var p in s.Products)
                                {
                                    result += $"\n--name: {p.Product.Name}\n--quantity: {p.OrderedQuantity}\n--price: {p.Product.Price}";
                                }
                                result += $"\nTotal price: {s.TotalSum:F2}";
                                result += $"\nProfit: {s.Profit:F2}";
                                result += "\n-------------------------------------";
                            }
                        }
                    }
                    break;
                default:
                    throw new InvalidOperationException($"{par} is invalid!");
            }


            return result;
        }
    }
}
