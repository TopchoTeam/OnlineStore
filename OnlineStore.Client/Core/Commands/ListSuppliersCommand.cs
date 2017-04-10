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
    public class ListSuppliersCommand
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
            using(OnlineStoreContext context=new OnlineStoreContext())
            {
                result += "--------------------------------------------------";
                var suppliers = context.Suppliers;
                foreach (var supplier in suppliers)
                {
                    result += $"\nname: {supplier.Name} ";

                    foreach(var product in supplier.Products)
                    {
                        result += $"\nproduct: {product.Name}";
                    }
                    result += "\n--------------------------------------------------";
                }
            }

            return result;
        }
    }
}
