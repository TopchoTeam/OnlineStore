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
            Console.WriteLine("Tip: search parameters are: SpecificTimePeriod/Today/All");
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

                        result += "-------------------------------------";
                        foreach (var s in sales)
                        {
                            result += $"\nDate: {s.OrderDate.Day}/{s.OrderDate.Month}/{s.OrderDate.Year}";
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
                    break;
                case "today":
                    DateTime date = DateTime.Now.Date;

                    using (OnlineStoreContext context = new OnlineStoreContext())
                    {
                        var sales = context.Sales.Where(s => s.OrderDate == date);

                        result += "-------------------------------------";
                        foreach (var s in sales)
                        {
                            result += $"\nDate: {s.OrderDate.Day}/{s.OrderDate.Month}/{s.OrderDate.Year}";
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
                    break;
                case "all":
                    using (OnlineStoreContext context = new OnlineStoreContext())
                    {
                        var sales = context.Sales;

                        result += "-------------------------------------";
                        foreach (var s in sales)
                        {
                            result += $"\nDate: {s.OrderDate.Day}/{s.OrderDate.Month}/{s.OrderDate.Year}";
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
                    break;
                default:
                    throw new InvalidOperationException($"{par} is invalid!");
            }


            return result;
        }
    }
}
