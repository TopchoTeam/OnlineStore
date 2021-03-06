﻿namespace OnlineStore.Client.Core
{
    using Commands;
    using System;

    public class CommandDispatcher
    {
        public string Dispatch(string input)
        {
            string result = string.Empty;

            string command = input.ToLower();

            switch (command)
            {
<<<<<<< HEAD
                case "listdeliveries":
                    ListDeliveriesCommand listDeliveries = new ListDeliveriesCommand();
                    result = listDeliveries.Execute();
=======
                case "activateuser":
                    ActivateUserCommand activateuser = new ActivateUserCommand();
                    result = activateuser.Execute();
>>>>>>> 48775038131c4d5d87228279db8ff4d0a28fedb6
                    break;
                case "transfermoney":
                    TransferMoneyCommand transferMoney = new TransferMoneyCommand();
                    result = transferMoney.Execute();
                    break;
                case "listsales":
                    ListSalesCommand listsales = new ListSalesCommand();
                    result = listsales.Execute();
                    break;
                case "supplyproducts":
                    SupplyProductsCommand supplyProducts = new SupplyProductsCommand();
                    result = supplyProducts.Execute();
                    break;
                case "viewmypurchases":
                    ViewMyPurchasesCommand viewmypurchases = new ViewMyPurchasesCommand();
                    result = viewmypurchases.Execute();
                    break;
                case "edituser":
                    EditUserCommand edituser = new EditUserCommand();
                    result = edituser.Execute();
                    break;
                case "deletemyprofile":
                    DeleteMyProfileCommand deleteMyProfile = new DeleteMyProfileCommand();
                    result = deleteMyProfile.Execute();
                    break;
                case "deleteuser":
                    DeleteUserCommand deleteUser = new DeleteUserCommand();
                    result = deleteUser.Execute();
                    break;
                case "listsuppliers":
                    ListSuppliersCommand listsuppliers = new ListSuppliersCommand();
                    result = listsuppliers.Execute();
                    break;
                case "editsupplier":
                    EditSupplierCommand editsupplier = new EditSupplierCommand();
                    result = editsupplier.Execute();
                    break;
                case "addsupplier":
                    AddSupplierCommand addsupplier = new AddSupplierCommand();
                    result = addsupplier.Execute();
                    break;
                case "purchaseproducts":
                    PurchaseProductsCommand purchaseproducts = new PurchaseProductsCommand();
                    result = purchaseproducts.Execute();
                    break;
                case "editproduct":
                    EditProductCommand editproduct = new EditProductCommand();
                    result = editproduct.Execute();
                    break;
                case "editmyprofile":
                    EditMyProfileCommand editmyprofile = new EditMyProfileCommand();
                    result = editmyprofile.Execute();
                    break;
                case "addproduct":
                    AddProductCommand addProduct = new AddProductCommand();
                    result = addProduct.Execute();
                    break;
                case "listproducts":
                    ListProductsCommand listproducts = new ListProductsCommand();
                    result = listproducts.Execute();
                    break;
                case "registeruser":
                    RegisterUserCommand registeruser = new RegisterUserCommand();
                    result = registeruser.Execute();
                    break;
                case "logout":
                    LogOutCommand logout = new LogOutCommand();
                    result = logout.Execute();
                    break;
                case "login":
                    LogInCommand login = new LogInCommand();
                    result = login.Execute();
                    break;
                case "listcommands":
                    ListCommandsCommand listCommands = new ListCommandsCommand();
                    result = listCommands.Execute();
                    break;
                case "exit":
                    ExitCommand exit = new ExitCommand();
                    exit.Execute();
                    break;
                default:
                    throw new NotSupportedException("Invalid Command!");
            }

            return result;
        }
    }
}
