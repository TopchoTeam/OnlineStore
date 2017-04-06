namespace OnlineStore.Client
{
    using Core;
    using Data;
    using Models;
    using System.Linq;

    class Application
    {
        static void Main(string[] args)
        {

           Engine engine = new Engine(new CommandDispatcher());
            engine.Run();
           
          
            /*var con = new OnlineStoreContext();
            var users = con.Users.ToList();

            foreach (var u in users)
            {
                System.Console.WriteLine($"{u.Account.AccountNumber} {u.UserName} {u.Account.Balance} {u.Account.AccountId}");
            }*/

        }
    }
}
