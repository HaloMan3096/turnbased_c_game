using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ServerResponseData
    {
        public bool success { get; set; }
        public int lID { get; set; }
        public LobbyData[]? lobbies { get; set; }

        public GameData? gameData { get; set; }
    }
}
