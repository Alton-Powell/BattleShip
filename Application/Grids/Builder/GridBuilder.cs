using Application.Interfaces;
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

            var battleShip = new Ship();
            var destroyer1 = new Ship();
            var destroyer2 = new Ship();

            grid.Ships.Add(battleShip);
            grid.Ships.Add(destroyer1);
            grid.Ships.Add(destroyer2);

            return grid;
        }
    }
}
