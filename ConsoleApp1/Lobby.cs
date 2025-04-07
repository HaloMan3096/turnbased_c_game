using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.Cryptography;

namespace ConsoleApp1
{
    internal class Lobby
    {
        public static Lobby Instance;

        LobbyData[] openLobbies = new LobbyData[0];

        public Lobby() 
        {
            Instance = this;
        }

        public async Task Run()
        {
            Console.WriteLine("Choose an option");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("R) Reload lobbies. Press a lobby ID to join the lobby");
            Console.WriteLine("C) Create lobby");
            Console.WriteLine("Or press a lobby ID to join the lobby");

            var input = System.Console.ReadLine();
            await HandleInput(input);
        }

        async Task HandleInput(string input)
        {
            switch(input)
            {
                case "r":
                case "R":
                    await Refresh();                    
                    break;
                case "c":
                case "C":
                    await CreateLobbyAndJoin();                   
                    break;
                default:

                    int lID = int.Parse(input);
                    LobbyData lobbyToJoin = null;

                    for(int i = 0; i < openLobbies.Length; i++)
                    {
                        if (openLobbies[i].id == lID)
                        {
                            lobbyToJoin = openLobbies[i];
                            break;
                        }
                    }

                    if (lobbyToJoin != null) await JoinLobby(lobbyToJoin.id.GetValueOrDefault(-1));
                    else await Run();

                    break;
            }
        }

        async Task Refresh()
        {
            Console.Clear();
            Console.WriteLine("Getting Lobbies");
            Console.WriteLine("------------------------------------");

            await ServerController.GetLobbies((lobbies) =>
            {
                openLobbies = lobbies;
            });

            if (openLobbies.Length > 0)
            {
                ShowLobbies();

                //TODO: ask user which game they would like to join
                //      and when they join make a server call with this user's id and the id of the 
                //      game they specified so the server knows they joined. then have the game
                //      play for the user.
                await Run();
            }
            else
            {
                await Run();
            }
            
        }

        async Task CreateLobbyAndJoin()
        {
            Console.Clear();
            Console.WriteLine("Creating Lobby...");

            int lID = -1;

            await ServerController.CreateLobby(UserManager.UserID, (_lID) => {

                Console.WriteLine("Lobby Created!");
                lID = _lID;
            });

            Game g = new Game(lID);
            await g.Join();
        }

        async Task JoinLobby(int lID)
        {
            await ServerController.JoinLobby(987,lID, () => {

                Console.WriteLine("Lobby Joined!");
            });

            Game g = new Game(lID);
            await g.Join();
        }

        void ShowLobbies()
        {
            for(int i = 0; i < openLobbies.Length; i++)
            {
                Console.WriteLine(openLobbies[i].ToString());
            }
        }        
    }
}
