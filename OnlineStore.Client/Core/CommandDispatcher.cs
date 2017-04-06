namespace OnlineStore.Client.Core
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
