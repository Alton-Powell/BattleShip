using Application;
using Application.Grids.Builder;
using Application.Interfaces;
using Common.Infrastructure;
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
            DisplayWelcomeAndAbout();
            Console.WriteLine("Enter a player name:.");
            var playerName = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Enter Coordinates A0-J9, or 'EXIT' to quit.");
            battleShipGame.StartGame(playerName);

            while (true)
            {
                var enteredText = Console.ReadLine().ToUpperInvariant();

                if (enteredText.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                    break;

                if (!ValidateInput(enteredText))
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }
                    
                
                
                //Validate entered Text, must be 2 Characters where
                //the first character is A-J
                //the second charcter is 0-9
                var isHit = battleShipGame.FireShot(enteredText);
                if (isHit)
                    Console.WriteLine("KAPOW!!! What a hit....");
                else
                    Console.WriteLine("AHHH!!! You missed....");

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
                _.For<IRandomNumberGenerator>().Use<RandomNumberGenerator>();
            });

            return container;
        }

        private static void DisplayWelcomeAndAbout()
        {
            Console.WriteLine("*********************************************");
            Console.WriteLine("*********************************************");
            Console.WriteLine("********Welcome to Battleship****************");
            Console.WriteLine("*3 Ships have been randomly placed on a grid*");
            Console.WriteLine("*The Grids have a squares position A0 - J9 **");
            Console.WriteLine("****************** Enjoy ********************");
        }

        private static bool ValidateInput(string targetedSquare)
        {
            if (string.IsNullOrWhiteSpace(targetedSquare) || targetedSquare.Length > 2)
                return false;

            if (targetedSquare[0] < 'A' || targetedSquare[0] > 'J')
                return false;

            if (targetedSquare[1] < '0' || targetedSquare[1] > '9')
                return false;

            return true;
        }

    }
}
