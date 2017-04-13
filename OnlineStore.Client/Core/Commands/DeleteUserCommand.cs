
namespace OnlineStore.Client.Core.Commands
{
using Utilities;
using Data;
using Models;
using System;
using System.Linq;
    public class DeleteUserCommand
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
            Console.Write("Enter UserName you want to delete: ");
            string userName = Console.ReadLine();
            using (OnlineStoreContext context = new OnlineStoreContext())
            {
                User user = context.Users.FirstOrDefault(u => u.UserName ==userName);
                if (user == null)
                {
                    throw new InvalidProgramException($"{userName} does not exist in database!");
                }
                if (user.IsDeleted == true)
                {
                    throw new InvalidOperationException($"User {user.UserName} is already deleted!");
                }
                user.IsDeleted = true;
                context.SaveChanges();
                result = $"User {user.UserName} deleted successfully!";
            }

            return result;
        }
    }
}
