namespace OnlineStore.Client.Core.Commands
{
    using Utilities;

    public class LogOutCommand
    {
        public string Execute()
        {
            string username = Authorization.Instance.Logout();

            return $"{username} has Logged Out!";
        }
    }
}
