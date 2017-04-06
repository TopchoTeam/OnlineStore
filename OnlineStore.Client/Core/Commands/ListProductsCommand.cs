namespace OnlineStore.Client.Core.Commands
{
    using Data;

    public class ListProductsCommand
    {
        public string Execute()
        {
            string result = string.Empty;
            using(OnlineStoreContext context = new OnlineStoreContext())
            {
                result += "--------------------------------------------------";
                var products = context.Products;
                foreach (var p in products)
                {
                    result += $"\nname: {p.Name} \nunit: {p.Unit} \nprice per unit: {p.Price} \non stock: {p.Quantity}";
                    result += "\n--------------------------------------------------";
                }
            }
            return result;
        }
    }
}
