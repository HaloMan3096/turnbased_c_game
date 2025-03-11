internal static class ServerController
{
    private static ServerCommunication ServerCommunication = new ServerCommunication();

    public static void TestServerResponse()
    {
        ServerCommunication.SendRequestASync([new UriParam("test", "true")], (response) =>
        {
            Console.WriteLine("HI");
        }, () =>
        {

        });
    }

    public static async Task TestSyncResponse()
    {
        await ServerCommunication.SendRequestSync([new UriParam("test", "true")], (response) =>
        {
            Console.WriteLine("HI");
        }, () =>
        {

        });            
    }

    public static async Task GetLobbies(Action<LobbyData[]> onComplete)
    {
        await ServerCommunication.SendRequestSync([new UriParam("get-lobbies", "true")], (response) =>
        {
            onComplete(response.response.lobbies);
        }, () =>
        {

        });
    }
}
