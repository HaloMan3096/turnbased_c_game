using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class LobbyData
    {
        public int? id { get; set; }
        public int? user1_id { get; set; }
        public int? user2_id { get; set; }

        public override string ToString()
        {
            int nUsers = 0;
            nUsers += user1_id != null ? 0 : 1;
            nUsers += user2_id != null ? 0 : 1;

            return string.Format("Lobby ID: {0}\t\t\tUsers: {1}/2",id,nUsers);
        }
    }
}
