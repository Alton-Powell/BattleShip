using Application.Interfaces;
using Domain.Ships;
using Domain;


namespace Application.Grids.Builder
{
    /// <summary>
    /// The Grid Builder creates the Grid for the Game
    /// this class implements the IGridBuilder which allows 
    /// for any number of different ways to create a game grid.
    /// </summary>

    public class GridBuilder : IGridBuilder
    {
        public Grid Create()
        {
            var grid = new Grid(10);

            var battleShip = new BattleShip();
            var destroyer1 = new Destroyer();
            var destroyer2 = new Destroyer();

            grid.Ships.Add(battleShip);
            grid.Ships.Add(destroyer1);
            grid.Ships.Add(destroyer2);

            grid.PositionShipsOnGrid();

            return grid;
        }
    }
}
