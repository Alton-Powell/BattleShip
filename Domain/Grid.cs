using Domain.Ships;
using System;
using System.Collections.Generic;


namespace Domain
{
    public class Grid
    {
        private ICollection<Ship> _ships;
        private readonly int _boundary;

        private ICollection<Square> _unavailbleSquare;

        public Grid(int boundary)
        {
            _boundary = boundary;
            _ships = new List<Ship>();

            _unavailbleSquare = new List<Square>();
        }

        public bool FireShotAtGrid(string targetedSquareCoordinate)
        {
            var targetedSquareOnGrid = new Square { ColumnRow = targetedSquareCoordinate };

            if (!_unavailbleSquare.Contains(targetedSquareOnGrid))
                return false;

            foreach(var ship in _ships)
            {
                if (ship.SetHit(targetedSquareOnGrid))
                    return true;
            }

            return false;
        }

        public bool AnyFloatingShips()
        {
            foreach (var ship in _ships)
            {
                if (ship.ShipState == ShipStatus.Afloat)
                    return true;
            }
            return false;
        }

        public ICollection<Ship> Ships
        {
            get { return _ships; }
        }

        public void PositionShipsOnGrid()
        {
            foreach (Ship ship in _ships)
            {
                char randomColumn = 'A';
                char randomRow = '0';
                bool canBePlaced = false;
                bool shipOrientationAcross = true;

                while (!canBePlaced)
                {
                    randomColumn = GetRandomColumn();
                    randomRow = GetRandomRow();

                    var random = new Random();
                    shipOrientationAcross = random.Next(2) == 0; //0 Across, 1 Down

                    canBePlaced = CanShipBePlaced(randomColumn, randomRow, ship.SpatialCapacity, shipOrientationAcross);

                    if(!canBePlaced)
                        canBePlaced = CanShipBePlaced(randomColumn, randomRow, ship.SpatialCapacity, !shipOrientationAcross);
                }
                PositionShipOnGrid(randomColumn, randomRow, ship, shipOrientationAcross);
            }
        }

        private void PositionShipOnGrid(char column, char row, Ship ship, bool placeShipAcross)
        {
            var columnRow = $"{column.ToString()}{row.ToString()}";
            var squareOnGrid = new Square { ColumnRow = columnRow };
            ship.SetShipPosition(squareOnGrid);

            _unavailbleSquare.Add(squareOnGrid);
            
            for (int i = 0; i < (ship.SpatialCapacity - 1); i++)
            {
                if (placeShipAcross)
                {
                    column = (char)(column + 1);
                    columnRow = $"{column.ToString()}{row.ToString()}";

                    squareOnGrid = new Square { ColumnRow = columnRow };

                    ship.SetShipPosition(squareOnGrid);
                    _unavailbleSquare.Add(squareOnGrid);
                }
                else
                {
                    row = (char)(row + 1);
                    columnRow = $"{column.ToString()}{row.ToString()}";

                    squareOnGrid = new Square { ColumnRow = columnRow };

                    ship.SetShipPosition(squareOnGrid);
                    _unavailbleSquare.Add(squareOnGrid);
                }
            }

        }

        private bool CanShipBePlaced(char column, char row, int spatialCapacity, bool placeShipAcross)
        {
            var columnRow = $"{column.ToString()}{row.ToString()}";

            if (_unavailbleSquare.Contains(new Square { ColumnRow = columnRow }))
                return false;

            for (int i = 0; i < (spatialCapacity-1); i++)
            {
                if (placeShipAcross)
                {
                    column = (char)(column + 1);
                    if (column > 'J')
                        return false;

                    columnRow = $"{column.ToString()}{row.ToString()}";
                    if (_unavailbleSquare.Contains(new Square { ColumnRow = columnRow }))
                        return false;
                }
                else
                {
                    row = (char)(row + 1);
                    if (row > '9')
                        return false;

                    columnRow = $"{column.ToString()}{row.ToString()}";
                    if (_unavailbleSquare.Contains(new Square { ColumnRow = columnRow }))
                        return false;
                }
            }
            return true;
        }

        private char GetRandomColumn()
        {
            var random = new Random();
            var randomNumber = random.Next(0, 9);
            return (char)('A' + randomNumber);
        }

        private char GetRandomRow()
        {
            var random = new Random();
            var randomNumber = random.Next(0, 9);
            return (char)('0' + randomNumber);
        }

    }
}
