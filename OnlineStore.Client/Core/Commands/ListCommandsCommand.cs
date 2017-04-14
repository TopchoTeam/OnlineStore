namespace OnlineStore.Client.Core.Commands
{
    using Utilities;
    using Models;
    public class ListCommandsCommand
    {
        public string Execute()
        {
            string result = string.Empty;
            
            if (Authorization.Instance.ValidateIsUserLoggedIn())
            {
                result += "RegisterUser";
                result += "\nListCommands";
                result += "\nPurchaseProducts";
                result += "\nEditMyProfile"; 
                result += "\nDeleteMyProfile";
                result += "\nViewMyPurchases";
                result += "\nListProducts";
                result += "\nTransferMoney"; 
                if (Authorization.Instance.CurrentUser.Role == UserRole.Admin)
                {
                    result += "\nAddProduct";
                    result += "\nAddSupplier"; 
                    result += "\nEditProduct"; 
                    result += "\nEditSupplier";
                    result += "\nListSupplier";
                    result += "\nActivateUser"; //TODO
                    result += "\nEditUser"; 
                    result += "\nDeleteUser";
                    result += "\nListSales"; 
                    result += "\nSupplyProducts";
                    result += "\nListDeliveries";//TODO
                }
                result += "\nLogOut";
                result += "\nExit";
            }
            else
            {
                result += "RegisterUser";
                result += "\nListCommands";
                result += "\nListProducts";
                result += "\nLogIn";
                result += "\nExit";
            }

            return result;
        }
    }
}
