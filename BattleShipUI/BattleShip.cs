using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipUI
{
    public class BattleShip
    {
        private readonly IGameEngine _gameEngine;

        public BattleShip(IGameEngine gameEngine)
        {
            _gameEngine = gameEngine ?? throw new ArgumentNullException(nameof(gameEngine));
        }

        public void StartGame(string playerName)
        {
            _gameEngine.InitializeGame(playerName);
        }

        public bool FireShot(string targetedSquare)
        {
            return _gameEngine.FireShot(targetedSquare);
        }

        public bool IsGameOver()
        {
            return _gameEngine.IsGameOver();
        }
    }
}
