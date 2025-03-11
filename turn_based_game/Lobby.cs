internal class Lobby
{
    LobbyData[] openLobbies = new LobbyData[0];

    public Lobby() { }

    public async Task Run()
    {
        Console.WriteLine("Press R to reload lobbies. Press a lobby ID to join the lobby");

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
            default:
                await Run();
                break;
        }
    }

    async Task Refresh()
    {
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

    void ShowLobbies()
    {
        for(int i = 0; i < openLobbies.Length; i++)
        {
            Console.WriteLine(openLobbies[i].ToString());
        }
    }        
}
