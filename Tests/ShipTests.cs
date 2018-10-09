using Common.Exceptions;
using Domain;
using Domain.Ships;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class ShipTests
    {
        private Ship _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Destroyer();// Use AutoMocker to get instance.
        }

        [Test]
        public void AddingSquares_GreaterThanShipSpatialCapacity_ThrowsInvalidShipSquareRequestException()
        {
            //Arrange
            _sut.SetShipPosition(new Square() { ColumnRow = "A1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "B1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "C1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "D1" });

            //Act
            TestDelegate test = () => _sut.SetShipPosition(new Square() { ColumnRow = "E1" });

            //Assert
            Assert.That(test, Throws.Exception.TypeOf<InvalidShipSquareRequest>());
        }

        [Test]
        public void GivenAFloatingShip_WhenTwoHits_SinksShip()
        {
            //Arrange
            _sut.SetShipPosition(new Square() { ColumnRow = "A1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "B1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "C1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "D1" });

            //Act
            _sut.SetHit(new Square { ColumnRow ="B1" });
            _sut.SetHit(new Square { ColumnRow = "C1" });

            //Assert
            Assert.That(_sut.ShipState, Is.EqualTo(ShipStatus.Sunk));
        }

        [Test]
        public void GivenAFloatingShip_WhenTwoHitsToTheSamePosition_ShipIsNotSunk()
        {
            //Arrange
            _sut.SetShipPosition(new Square() { ColumnRow = "A1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "B1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "C1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "D1" });

            //Act
            _sut.SetHit(new Square { ColumnRow = "B1" });
            _sut.SetHit(new Square { ColumnRow = "B1" });

            //Assert
            Assert.That(_sut.ShipState, Is.Not.EqualTo(ShipStatus.Sunk));
        }

        [Test]
        public void GivenAFloatingShip_ShipNotInTargetedSquare_HitReturnsFalse()
        {
            //Arrange
            _sut.SetShipPosition(new Square() { ColumnRow = "A1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "B1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "C1" });
            _sut.SetShipPosition(new Square() { ColumnRow = "D1" });

            //Act
            var hitRecorded = _sut.SetHit(new Square { ColumnRow = "E2" });
           
            //Assert
            Assert.That(hitRecorded, Is.False);
        }

    }
}
