
namespace OnlineStore.Client.Core.Commands
{
using OnlineStore.Client.Utilities;
using OnlineStore.Data;
using OnlineStore.Models;
using System;
using System.Linq;
    public class DeleteMyProfileCommand
    {
        public string Execute()
        {
            if (!Authorization.Instance.ValidateIsUserLoggedIn())
            {
                throw new InvalidOperationException("You should log in first for this operation");
            }
            string result = string.Empty;
            Console.Clear();
            using (OnlineStoreContext context = new OnlineStoreContext())
            {
                User user = context.Users.SingleOrDefault(u => u.UserName == Authorization.Instance.CurrentUser.UserName);

                if (user.IsDeleted == true)
                {
                    throw new InvalidOperationException($"User {user.UserName} is already deleted!");
                }
                user.IsDeleted = true;
                context.SaveChanges();
                Authorization.Instance.Logout();
                result = $"User {user.UserName} deleted successfully!";
            }
            return result;
          
        }
    }
}
