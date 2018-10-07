using Common.Exceptions;
using System.Collections.Generic;

namespace Domain.Ships
{
    public abstract class Ship
    {
        protected ICollection<Square> _shipSquares;
        protected int _spatialCapacity;

        public ShipStatus ShipState { get; set; }

        public int SpatialCapacity
        {
            get { return _spatialCapacity; }
        }

        public void SetShipPosition(Square shipPosition)
        {
            if (_shipSquares.Count < _spatialCapacity)
                _shipSquares.Add(shipPosition);
            else
                throw new InvalidShipSquareRequest($"Invalid ship position request for {shipPosition.ColumnRow}");
        }

        public bool SetHit(Square targetedSquare)
        {
            if (!_shipSquares.Contains(targetedSquare))
                return false;

            foreach(var shipSquare in _shipSquares)
            {
                if (shipSquare.Equals(targetedSquare))
                {
                    shipSquare.IsHit = true;
                    UpdateShipState();
                    return true;
                }
            }
            return false;
        }

        private void UpdateShipState()
        {
            if (ShipState == ShipStatus.Afloat)
            {
                ShipState = ShipStatus.Damaged;
                return;
            }
            if (ShipState == ShipStatus.Damaged)
            {
                ShipState = ShipStatus.Sunk;
                return;
            }
        }
   
    }
}
