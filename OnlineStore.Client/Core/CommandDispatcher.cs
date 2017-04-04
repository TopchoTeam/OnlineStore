namespace OnlineStore.Client.Core
{
    using Commands;
    using System;

    public class CommandDispatcher
    {
        public string Dispatch(string input)
        {
            string result = string.Empty;

            string command = input.ToLower();

            switch (command)
            {
                case "login":
                    LogInCommand login = new LogInCommand();
                    result = login.Execute();
                    break;
                case "listcommands":
                    ListCommandsCommand listCommands = new ListCommandsCommand();
                    result = listCommands.Execute();
                    break;
                case "exit":
                    ExitCommand exit = new ExitCommand();
                    exit.Execute();
                    break;
                default:
                    throw new NotSupportedException("Invalid Command!");
            }

            return result;
        }
    }
}
