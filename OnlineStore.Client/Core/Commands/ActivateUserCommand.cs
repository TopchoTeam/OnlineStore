namespace OnlineStore.Client.Core.Commands
{
    using System;
    using Utilities;
    using Models;
    using Data;
    using System.Linq;

    public class ActivateUserCommand
    {
        public string Execute()
        {
            if (!Authorization.Instance.ValidateIsUserLoggedIn())
            {
                throw new InvalidOperationException("You should log in first for this operation");
            }
            if (Authorization.Instance.CurrentUser.Role != UserRole.Admin)
            {
                throw new InvalidOperationException("You don't have access to this operation!");
            }

            string result = string.Empty;
            Console.Clear();
            Console.Write("Enter username: ");
            string name = Console.ReadLine().ToLower();
            using (OnlineStoreContext context = new OnlineStoreContext())
            {
                if (!context.Users.Any(u => u.UserName.ToLower() == name && u.IsDeleted == true))
                {
                    throw new InvalidProgramException($"{name} is active or doesn't exist.");
                }

                User user = context.Users.FirstOrDefault(u => u.UserName.ToLower() == name);
                user.IsDeleted = false;
                context.SaveChanges();
                result = $"{name} is activated!";
            }

            return result;
        }
    }
}
