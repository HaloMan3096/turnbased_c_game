using ConsoleApp1;

class Program
{
    static async Task<int> Main()
    {
        Console.WriteLine("What's your User ID?");

        var input = int.Parse(System.Console.ReadLine());
        UserManager.Login(input);

        Lobby lobby = new Lobby();
        await lobby.Run();

        return 0;
    }
}