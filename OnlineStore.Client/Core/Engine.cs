namespace OnlineStore.Client.Core
{
    using System;

    public class Engine
    {
        private CommandDispatcher commandDispatcher;

        public Engine(CommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public void Run()
        {
            while (true)
            {
              try
                {
                    Console.WriteLine("Tip: To see available commands type \"ListCommands\"");
                    Console.Write("Enter Command: ");
                    string input = Console.ReadLine();
                    string output = this.commandDispatcher.Dispatch(input);

                    Console.Clear();
                    Console.WriteLine(output);
                }
                catch(Exception e)
                {
                    Console.Clear();
                    //Console.WriteLine($"Invalid Command!");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
