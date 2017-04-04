namespace OnlineStore.Client.Core.Commands
{
    using System;
    using Utilities;

    public class LogInCommand
    {

        public string Execute()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            Authorization.Instance.Login(username, password);

            return $"{username} has Logged In!";
        }
    }
}
