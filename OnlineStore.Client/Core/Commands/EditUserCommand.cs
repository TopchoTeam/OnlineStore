namespace OnlineStore.Client.Core.Commands
{
    using Models;
    using System;
    using Utilities;
    using Data;
    using System.Linq;

    public class EditUserCommand
    {
        public string Execute()
        {
            string result = string.Empty;

            if (!Authorization.Instance.ValidateIsUserLoggedIn())
            {
                throw new InvalidOperationException("You should log in first for this operation");
            }
            if (Authorization.Instance.CurrentUser.Role != UserRole.Admin)
            {
                throw new InvalidOperationException("You don't have access to this operation!");
            }

            Console.Clear();
            Console.WriteLine("Tip: you can change Password/UserRole.");
            Console.Write("Enter property: ");
            string prop = Console.ReadLine().ToLower();

            switch (prop)
            {
                case "password":
                    Console.Clear();
                    Console.Write("Enter Username: ");
                    string username = Console.ReadLine().ToLower();

                    using(OnlineStoreContext context = new OnlineStoreContext())
                    {
                        if (!context.Users.Any(u => u.UserName == username && u.IsDeleted == false))
                        {
                            throw new InvalidOperationException($"User {username} does not exist.");
                        }

                        Console.Write("Enter new password: ");
                        string password = Console.ReadLine();
                        User user = context.Users.FirstOrDefault(u => u.UserName == username);
                        user.Password = password;
                        context.SaveChanges();
                        result = $"User {username} {prop} was changed successfully.";
                    }
                    break;
                case "userrole":
                    Console.Clear();
                    Console.Write("Enter Username: ");
                    string name = Console.ReadLine().ToLower();

                    using(OnlineStoreContext context = new OnlineStoreContext())
                    {
                        if (!context.Users.Any(u => u.UserName == name && u.IsDeleted == false))
                        {
                            throw new InvalidOperationException($"User {name} does not exist.");
                        }
                        Console.Clear();
                        Console.Write("Enter new role(Admin/Customer): ");
                        string role = Console.ReadLine().ToLower();

                        switch (role)
                        {
                            case "admin":
                                User us = context.Users.FirstOrDefault(u => u.UserName == name);
                                us.Role = UserRole.Admin;
                                break;
                            case "customer":
                                User use = context.Users.FirstOrDefault(u => u.UserName == name);
                                use.Role = UserRole.Customer;
                                break;
                            default:
                                throw new InvalidProgramException($"{role} is invalid.");
                        }
                        context.SaveChanges();

                        result = $"User {name} {prop} was changed successfully.";
                    }
                    break;
                default:
                    throw new InvalidProgramException($"{prop} is invalid.");
            }
            return result;
        }
    }
}
