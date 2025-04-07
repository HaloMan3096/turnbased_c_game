using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
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

        public static async Task CreateLobby(int uID, Action<int> onComplete)
        {
            UriParam[] uriParams = [
                new UriParam("create-lobby", "true"),
                new UriParam("user-id", uID.ToString())
            ];

            await ServerCommunication.SendRequestSync(uriParams, (response) =>
            {
                onComplete(response.response.lID);
            }, () =>
            {

            });
        }

        public static async Task ExitLobby(int uID, int lID, Action onComplete)
        {
            UriParam[] uriParams = [
                new UriParam("exit-lobby", "true"),
                new UriParam("user-id", uID.ToString()),
                new UriParam("lobby-id", lID.ToString())
            ];

            await ServerCommunication.SendRequestSync(uriParams, (response) =>
            {
                onComplete();
            }, () =>
            {

            });
        }

        public static async Task JoinLobby(int uID, int lID, Action onComplete)
        {
            UriParam[] uriParams = [
                new UriParam("join-lobby", "true"),
                new UriParam("user-id", uID.ToString()),
                new UriParam("lobby-id", lID.ToString())
            ];

            await ServerCommunication.SendRequestSync(uriParams, (response) =>
            {
                onComplete();
            }, () =>
            {
                
            });
        }

        public static async Task TakeTurn(int gID,int answer, Action onComplete)
        {
            UriParam[] uriParams = [
                new UriParam("take-turn", "true"),
                new UriParam("game-id", gID.ToString()),
                new UriParam("answer", answer.ToString())
            ];

            await ServerCommunication.SendRequestSync(uriParams, (response) =>
            {
                onComplete();
            }, () =>
            {

            });
        }

        public static async Task CheckGameStatus(int gID, Action<GameData> onComplete)
        {
            UriParam[] uriParams = [
                new UriParam("check-game-status", "true"),
                new UriParam("game-id", gID.ToString())
            ];

            await ServerCommunication.SendRequestSync(uriParams, (response) =>
            {
                onComplete(response.response.gameData);
            }, () =>
            {

            });
        }
    }
}
