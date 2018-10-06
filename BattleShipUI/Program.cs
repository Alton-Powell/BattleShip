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

            Console.WriteLine("Enter Coordinates, or 'EXIT' to quit.");
            battleShipGame.StartGame();

            while (true)
            {
                var enteredText = Console.ReadLine();
                if (enteredText.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                    break;

                

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
