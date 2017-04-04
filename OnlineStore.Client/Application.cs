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
            //var con = new OnlineStoreContext();
            //con.Database.Initialize(true);
        }
    }
}
