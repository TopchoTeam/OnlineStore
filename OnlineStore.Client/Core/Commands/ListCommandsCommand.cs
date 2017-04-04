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
                result += "ListCommands";
                result += "\nPurchaseProducts"; //TODO
                result += "\nEditMyProfile"; //TODO
                result += "\nDeleteMyProfile"; //TODO
                result += "\nViewMyPurchases"; //TODO
                result += "\nListProducts"; //TODO
                result += "\nEditPurchase"; //TODO
                result += "\nTransferMoney"; //TODO
                if (Authorization.Instance.CurrentUser.Role == UserRole.Admin)
                {
                    result += "\nAddProduct"; //TODO
                    result += "\nAddSupplier"; //TODO
                    result += "\nEditProduct"; //TODO
                    result += "\nEditSupplier"; //TODO
                    result += "\nDeleteUser"; //TODO
                    result += "\nListSales"; //TODO
                }
                result += "\nLogOut"; //TODO
                result += "\nExit";
            }
            else
            {
                result += "RegisterUser"; //TODO
                result += "\nListCommands";
                result += "\nListProducts"; //TODO
                result += "\nLogIn";
                result += "\nExit";
            }

            return result;
        }
    }
}
