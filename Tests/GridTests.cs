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
    public class GridTests
    {
        private Mock<IRandomNumberGenerator> _mockRandomNumberGenerator;

        private Grid _sut;

        [SetUp]
        public void Setup()
        {
            _mockRandomNumberGenerator = new Mock<IRandomNumberGenerator>();
            _mockRandomNumberGenerator.SetupSequence(x => x.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1).Returns(1).Returns(0);
            _mockRandomNumberGenerator.Setup(x => x.Next(It.IsAny<int>())).Returns(0);
            _sut = new Grid(10, _mockRandomNumberGenerator.Object); //could use automoq to get instance

            
        }

        [Test]
        public void GivenAShipToBePlacedAcross_PositionIsAvialable_ShipIsPositioned()
        {
            //Arrange
            var battleShip = new BattleShip();
            _sut.Ships.Add(battleShip);
            

            //Act
            _sut.PositionShipsOnGrid();

            //Assert
            _mockRandomNumberGenerator.Verify(x => x.Next(0, 9), Times.Exactly(2));
            Assert.That(battleShip.ShipSquares, Is.Not.Null);
        }

        [Test]
        public void GivenTwoShipToBePlacedAcross_PositionIsNotAvailable_SecondShipIsPositionedDown()
        {
            //Arrange
            _mockRandomNumberGenerator.SetupSequence(x => x.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(3).Returns(0).Returns(0).Returns(0);

            var battleShip = new BattleShip();
            _sut.Ships.Add(battleShip);

            var destroyer = new Destroyer();
            _sut.Ships.Add(destroyer);

           //Act
           _sut.PositionShipsOnGrid();

            //Assert
            _mockRandomNumberGenerator.Verify(x => x.Next(0, 9), Times.Exactly(4));
            Assert.That(destroyer.ShipSquares[1].ColumnRow, Is.EqualTo("A1"));
        }

        [Test]
        public void GivenAShipIsAfloat_Calling_AnyFloatingShip_ReturnsTrue()
        {
            //Arrange
             var battleShip = new BattleShip
             {
                 ShipState = ShipStatus.Afloat
             };
            _sut.Ships.Add(battleShip);
            var destroyer = new Destroyer
            {
                ShipState = ShipStatus.Damaged
            };
            _sut.Ships.Add(destroyer);

            //Act
            var anyFloatingShipsLeft = _sut.AnyFloatingShips();

            //Assert
            Assert.That(anyFloatingShipsLeft, Is.EqualTo(true));
        }

        [Test]
        public void GivenAllShipsAreSunk_Calling_AnyFloatingShip_ReturnsFalse()
        {
            //Arrange
            var battleShip = new BattleShip
            {
                ShipState = ShipStatus.Sunk
            };
            _sut.Ships.Add(battleShip);
            var destroyer = new Destroyer
            {
                ShipState = ShipStatus.Sunk
            };
            _sut.Ships.Add(destroyer);

            //Act
            var anyFloatingShipsLeft = _sut.AnyFloatingShips();

            //Assert
            Assert.That(anyFloatingShipsLeft, Is.EqualTo(false));
        }

        [Test]
        public void GivenAnEmptyGrid_FireShotAtGrid_ReturnsFalse()
        {
            //Act
            var hitShip = _sut.FireShotAtGrid("A1");

            //Assert
            Assert.That(hitShip, Is.False);
        }

        [Test]
        public void GivenAShipOnGrid_FireShotAtGridMissingShip_ReturnsFalse()
        {
            //Arrange
            var battleShip = new BattleShip
            {
                ShipState = ShipStatus.Afloat
            };
            _sut.Ships.Add(battleShip);

            _sut.PositionShipsOnGrid();

            //Act
            var hitShip = _sut.FireShotAtGrid("A1");

            //Assert
            Assert.That(hitShip, Is.False);
        }

        [Test]
        public void GivenAShipOnGrid_FireShotAtGridHittingShip_ReturnsTrueAndIsDamaged()
        {
            //Arrange
            var battleShip = new BattleShip
            {
                ShipState = ShipStatus.Afloat
            };
            _sut.Ships.Add(battleShip);

            _sut.PositionShipsOnGrid();

            //Act
            var hitShip = _sut.FireShotAtGrid("B1");

            //Assert
            Assert.That(hitShip, Is.True);
            Assert.That(battleShip.ShipState, Is.EqualTo(ShipStatus.Damaged));
        }
    }
}
