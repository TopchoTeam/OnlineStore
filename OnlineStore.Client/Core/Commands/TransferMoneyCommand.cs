namespace OnlineStore.Client.Core.Commands
{
    using System;
    using Utilities;
    using Models;
    using Data;
    using System.Linq;

    public class TransferMoneyCommand
    {
        public string Execute()
        {
            if (!Authorization.Instance.ValidateIsUserLoggedIn())
            {
                throw new InvalidOperationException("You should log in first for this operation");
            }

            Console.Clear();
            string result = string.Empty;
            Console.Write("Enter transfer type(Withdrawal/Deposit): ");
            string tran = Console.ReadLine().ToLower();
            Console.Clear();
            Console.Write("Enter money amount: ");
            decimal money = decimal.Parse(Console.ReadLine());

            using(OnlineStoreContext context = new OnlineStoreContext())
            {
                User user = context.Users.FirstOrDefault(u => u.UserName == Authorization.Instance.CurrentUser.UserName);

                switch (tran)
                {
                    case "withdrawal":
                        if (money > user.Account.Balance)
                        {
                            throw new InvalidOperationException("Not enough money in your account!");
                        }
                        user.Account.Balance -= money;
                        context.SaveChanges();
                        break;
                    case "deposit":
                        user.Account.Balance += money;
                        context.SaveChanges();
                        break;
                    default:
                        throw new InvalidOperationException($"{tran} is invalid!");
                }

                result = $"{tran} is successful.";
            }

            return result;
        }
    }
}
