using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ships
{
    public class Destroyer : Ship
    {
        public Destroyer()
        {
            _spatialCapacity = 4;
            _shipSquares = new List<Square>();
        }
    }
}
