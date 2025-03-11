public class LobbyData
{
    public string? id { get; set; }
    public string? user1_id { get; set; }
    public string? user2_id { get; set; }

    public override string ToString()
    {
        int nUsers = 0;
        nUsers += string.IsNullOrEmpty(user1_id) ? 0 : 1;
        nUsers += string.IsNullOrEmpty(user2_id) ? 0 : 1;

        return string.Format("Lobby ID: {0}\t\t\tUsers: {1}/2",id,nUsers);
    }
}
