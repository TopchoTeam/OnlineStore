namespace OnlineStore.Client.Core.Commands
{
    using System;
    using Utilities;
    using Data;
    using Models;
    using System.Linq;

    public class EditMyProfileCommand
    {
        public string Execute()
        {
            if (!Authorization.Instance.ValidateIsUserLoggedIn())
            {
                throw new InvalidOperationException("You should log in first for this operation");
            }
            string result = string.Empty;
            Console.Clear();
            Console.WriteLine("Tip: you can change your Password/Email/DeliveryAddres.");
            Console.Write("Enter property: ");
            string prop = Console.ReadLine().ToLower();
            using(OnlineStoreContext context = new OnlineStoreContext())
            {
                User user = context.Users.FirstOrDefault(u => u.UserName == Authorization.Instance.CurrentUser.UserName);
                
                switch (prop)
                {
                    case "password":
                        Console.Clear();
                        Console.Write("Enter new password: ");
                        string pass1 = Console.ReadLine();
                        Console.Write("Confirm new password: ");
                        string pass2 = Console.ReadLine();
                        if (pass1 != pass2)
                        {
                            throw new InvalidOperationException("Passwords don't match!");
                        }
                        user.Password = pass1;
                        break;
                    case "email":
                        Console.Clear();
                        Console.Write("Enter email: ");
                        string email = Console.ReadLine();
                        if (context.Users.Any(u => u.Email.ToLower() == email.ToLower()))
                        {
                            throw new InvalidProgramException($"{email} already is taken!");
                        }
                        user.Email = email;
                        break;
                    case "deliveryaddres":
                        Console.Clear();
                        Console.Write("Enter addres: ");
                        string addres = Console.ReadLine();
                        user.Address = addres;
                        break;
                    default:
                        throw new InvalidOperationException($"{prop} is invalid!");
                }
                context.SaveChanges();

                result = $"{prop} changed successfully!";
            }
            
            return result;
        }
    }
}
