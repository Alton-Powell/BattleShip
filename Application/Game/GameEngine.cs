using Application.Interfaces;
using Domain;
using System;

namespace Application
{
    public class GameEngine : IGameEngine
    {

        private readonly IGridBuilder _gridBuilder;

        public GameEngine(IGridBuilder gridBuilder)
        {
            _gridBuilder = gridBuilder ?? throw new ArgumentNullException(nameof(gridBuilder));
        }

        public void InitializeGame(string playerName)
        {
            Player player = new Player(playerName);
            Grid grid = _gridBuilder.Create();

            Game game = new Game();
            game.Player = player;
            game.Grid = grid;

            return game;
        }
    }
}
