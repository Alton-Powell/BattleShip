using Common.Infrastructure;
using Domain;
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
                .Returns(1).Returns(2).Returns(3);
            _mockRandomNumberGenerator.Setup(x => x.Next(It.IsAny<int>())).Returns(0);
            _sut = new Grid(10, _mockRandomNumberGenerator.Object); //could use automoq to get instance
        }

        [Test]
        public void GivenThreeShips_WhenPositionShipsOnGridIsCalled_AllShipsHaveSquares()
        {
            //Arrange

            //Act
            _sut.PositionShipsOnGrid();

            //Assert

        }
    }
}
