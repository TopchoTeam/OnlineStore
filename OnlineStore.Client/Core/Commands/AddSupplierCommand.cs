namespace OnlineStore.Client.Core.Commands
{
    using Utilities;
    using Data;
    using Models;
    using System;
    using System.Linq;

    public class AddSupplierCommand
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
            Console.Write("Enter supplier name: ");
            string supplierName = Console.ReadLine().ToLower();
            using(OnlineStoreContext context=new OnlineStoreContext())
            {
                var supplierByName = context.Suppliers.FirstOrDefault(s => s.Name == supplierName);
                if (supplierByName != null)
                {
                    throw new Exception($"Supplier {supplierName} already exist in database!");
                }

                Supplier supplier = new Supplier()
                {
                    Name = supplierName
                };

                context.Suppliers.Add(supplier);
                context.SaveChanges();

                result = $"{supplierName} was added succesfully to database";
            }
            return result;
        }
    }
}
