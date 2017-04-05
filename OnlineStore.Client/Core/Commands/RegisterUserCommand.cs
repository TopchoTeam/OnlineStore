namespace OnlineStore.Client.Core.Commands
{
    using System;
    using Models;
    using Data;
    using System.Linq;

    public class RegisterUserCommand
    {
        public string Execute()
        {
            
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            Console.Write("Confirm Password: ");
            string password2 = Console.ReadLine();
            if (password != password2)
            {
                throw new InvalidOperationException("Passwords don't match!");
            }
            Console.Clear();
            Console.Write("Enter FirstName: ");
            string firstname = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter LastName: ");
            string lastname = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter DelivaryAddres: ");
            string addres = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter AccountNumber: ");
            string accountNumber = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter AccountBalance: ");
            decimal balance = decimal.Parse(Console.ReadLine());

            using(OnlineStoreContext context = new OnlineStoreContext())
            {
                if (context.Users.Any(u => u.UserName == username))
                {
                    throw new InvalidOperationException($"{username} already exists!");
                }
                if (context.Users.Any(u => u.Email == email))
                {
                    throw new InvalidOperationException($"{email} already exists!");
                }
                if (context.Accounts.Any(a => a.AccountNumber == accountNumber))
                {
                    throw new InvalidOperationException($"{accountNumber} already exists!");
                }
                Account account = new Account();
                account.AccountNumber = accountNumber;
                account.Balance = balance;
                context.Accounts.Add(account);

                User user = new User();
                user.UserName = username;
                user.FirstName = firstname;
                user.LastName = lastname;
                user.Password = password;
                user.Email = email;
                user.Address = addres;
                user.IsDeleted = false;
                user.Account = account;
                user.Role = UserRole.Customer;

                context.Users.Add(user);
                context.SaveChanges();
            }

            return $"{username} has registered successfully";
        }
    }
}
