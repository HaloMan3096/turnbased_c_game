class Program
{
    static async Task<int> Main()
    {
        Console.WriteLine("Hello, World!");

        var lobby = new Lobby();
        await lobby.Run();

        return 0;
    }
}