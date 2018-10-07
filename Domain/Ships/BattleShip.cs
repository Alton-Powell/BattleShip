using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ships
{
    public class BattleShip : Ship
    {
        public BattleShip()
        {
            _spatialCapacity = 5;
            _shipSquares = new List<Square>();
        }
    }
}
