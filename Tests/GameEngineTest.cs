using Application;
using Application.Interfaces;
using Common.Infrastructure;
using Domain;
using Domain.Ships;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class GameEngineTest
    {
        private GameEngine _sut;
        private Mock<IGridBuilder> _mockGridBuilder;

        [SetUp]
        public void Setup()
        {
            var randomGenerator = Mock.Of<IRandomNumberGenerator>();
            _mockGridBuilder = new Mock<IGridBuilder>();

            var grid = new Grid(10, randomGenerator);

            Ship destroyer = new Destroyer();
            destroyer.SetShipPosition(new Square() { ColumnRow = "A1" });
            grid.UnavailbleSquare.Add(new Square { ColumnRow = "A1" });
            destroyer.SetShipPosition(new Square() { ColumnRow = "B1" });
            grid.UnavailbleSquare.Add(new Square { ColumnRow = "B1" });
            destroyer.SetShipPosition(new Square() { ColumnRow = "C1" });
            grid.UnavailbleSquare.Add(new Square { ColumnRow = "C1" });
            destroyer.SetShipPosition(new Square() { ColumnRow = "D1" });
            grid.UnavailbleSquare.Add(new Square { ColumnRow = "D1" });

            Ship battleShip = new BattleShip();
            battleShip.SetShipPosition(new Square() { ColumnRow = "A2" });
            grid.UnavailbleSquare.Add(new Square { ColumnRow = "A2" });
            battleShip.SetShipPosition(new Square() { ColumnRow = "B2" });
            grid.UnavailbleSquare.Add(new Square { ColumnRow = "B2" });
            battleShip.SetShipPosition(new Square() { ColumnRow = "C2" });
            grid.UnavailbleSquare.Add(new Square { ColumnRow = "C2" });
            battleShip.SetShipPosition(new Square() { ColumnRow = "D2" });
            grid.UnavailbleSquare.Add(new Square { ColumnRow = "D2" });
            battleShip.SetShipPosition(new Square() { ColumnRow = "E2" });
            grid.UnavailbleSquare.Add(new Square { ColumnRow = "E2" });

            grid.Ships.Add(battleShip);
            grid.Ships.Add(destroyer);

            _mockGridBuilder.Setup(x => x.Create()).Returns(grid);

            _sut = new GameEngine(_mockGridBuilder.Object);
        }

        [Test]
        public void InitializeGame_CreatesGrid()
        {
            //Act
            _sut.InitializeGame("Bob");

            //Assert
            _mockGridBuilder.Verify(x => x.Create(), Times.Once);
        }

        [Test]
        public void AllShipSunk_GameOver_ReturnsTrue()
        {
            //Arrange
            _sut.InitializeGame("Bob");

            _sut.FireShot("A1");
            _sut.FireShot("B1");
            _sut.FireShot("B2");
            _sut.FireShot("C2");

            //Act
            var isGameOver = _sut.IsGameOver();

            //Assert
            Assert.That(isGameOver, Is.True);
        }

        [Test]
        public void NotAllShipSunk_GameOver_ReturnsFalse()
        {
            //Arrange
            _sut.InitializeGame("Bob");

            _sut.FireShot("A1");
            _sut.FireShot("B1");
            _sut.FireShot("B2");

            //Act
            var isGameOver = _sut.IsGameOver();

            //Assert
            Assert.That(isGameOver, Is.False);
        }
    }
}
