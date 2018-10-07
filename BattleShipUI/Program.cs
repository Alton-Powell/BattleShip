using Application;
using Application.Grids.Builder;
using Application.Interfaces;
using StructureMap;
using System;

namespace BattleShipUI
{
    class Program
    {
        static void Main(string[] args)
        {

            // this is required to wire up my types
            var iocContainer = InitializeIoC();

            var battleShipGame = iocContainer.GetInstance<BattleShip>();

            Console.WriteLine("Enter a player name:.");
            var playerName = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Enter Coordinates, or 'EXIT' to quit.");
            battleShipGame.StartGame(playerName);

            while (true)
            {
                var enteredText = Console.ReadLine();
                if (enteredText.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                    break;
                
                //Validate entered Text, must be 2 Characters where
                //the first character is A-J
                //the second charcter is 0-9
                var isHit = battleShipGame.FireShot(enteredText);
                if (isHit)
                {
                    Console.WriteLine("KAPOW!!! What a hit....");
                }

                var gameOver = battleShipGame.IsGameOver();

                if (gameOver)
                {
                    Console.WriteLine("Congratulations!!!....All ships sunk");
                    Console.ReadKey();
                    break;
                }
            }

        }

        private static Container InitializeIoC()
        {
            var container = new Container(_ =>
            {
                _.Scan(x =>
                {
                    x.AssembliesFromApplicationBaseDirectory(
                        filter => filter.FullName.StartsWith("BattleShip")); 
                    x.WithDefaultConventions();
                });

                _.For<IGameEngine>().Use<GameEngine>();
                _.For<IGridBuilder>().Use<GridBuilder>();
            });

            return container;
        }

    }
}
