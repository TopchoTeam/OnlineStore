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
    public class EditSupplierCommand
    {
        public string Execute()
        {
            if (!Authorization.Instance.ValidateIsUserLoggedIn())
            {
                throw new InvalidOperationException("You should log in first for this operation!");
            }
            if(Authorization.Instance.CurrentUser.Role!=UserRole.Admin)
            {
                throw new InvalidOperationException("You don't have access to this operation!");
            }

            string result = string.Empty;
            Console.Clear();
            Console.Write("Enter supplier name: ");
            string supplierName = Console.ReadLine().ToLower();
            using (OnlineStoreContext context = new OnlineStoreContext())
            {
                var supplierByName = context.Suppliers.FirstOrDefault(s => s.Name == supplierName);
                if (supplierByName == null)
                {
                    throw new Exception($"Supplier {supplierName} does not exist in database!");
                }

                Console.Clear();
                Console.Write("Enter new supplier name: ");
                string supplierNewName = Console.ReadLine();
                supplierByName.Name = supplierNewName;

                context.SaveChanges();
                result = $"Supplier {supplierNewName} was edited successfully!";
            }

            return result;
        }
    }
}
