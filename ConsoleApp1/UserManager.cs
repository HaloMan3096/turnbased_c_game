namespace ConsoleApp1
{
    internal static class UserManager
    {
        public static int UserID { get; private set;}

        public static void Login(int userID)
        {
            UserID = userID;
        }
    }
}
