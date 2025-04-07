using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Game
    {
        int id;

        bool isPlayer1;
        bool isPlayer2;

        public Game(int id) 
        {
            this.id = id;
        }

        public async Task Join()
        {
            Console.Clear();
            Console.WriteLine("Choose an option");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("R) Reload Game");
            Console.WriteLine("E) Exit Game");

            var input = System.Console.ReadLine();
            await HandleInput(input);


            //enter waiting state on a loop keep calling ServerController.CheckGameStatus and
            //if active == 0 then game hasn't started yet and just keep checking.

            //otherwise if active is 1 then check to see if turn_id == UserManager.UserID
            //if it matches UserID then ask the user to enter a number 1-10 and call ServerController.TakeTurn
            //don't forget to pass in the lobby-id which is also the game-id
            //immediately go into the loop checking for checking CheckGameStatus and repeat the above logic

            //if winner_id != null and == userid then tell the user they won and do whatever you want ie ask them to exit the lbby
            //or kick them out automatically etc.

            //if the winner_id !- null and it does not match userid then call them a loser and kick them out or whatever
        }

        async Task HandleInput(string input)
        {
            switch (input)
            {
                case "r":
                case "R":
                    //await Refresh();
                    break;
                case "e":
                case "E":
                    await Exit();
                    break;
                default:
                    //await Run();
                    break;
            }
        }

        async Task Exit()
        {
            await ServerController.ExitLobby(UserManager.UserID, id, () =>
            {

            });

            await Lobby.Instance.Run();
        }

    }
}
