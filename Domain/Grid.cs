using Domain.Ships;
using System.Collections.Generic;


namespace Domain
{
    public class Grid
    {
        private ICollection<Ship> _ships;
        private readonly int _boundary; 

        public Grid(int boundary)
        {
            _boundary = boundary;
            _ships = new List<Ship>();
        }

        public ICollection<Ship> Ships
        {
            get { return _ships; }
        }

    }
}
