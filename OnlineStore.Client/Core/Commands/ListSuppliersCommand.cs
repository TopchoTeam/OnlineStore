namespace OnlineStore.Client.Core.Commands
{
    using Utilities;
    using Data;
    using Models;
    using System;

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
