using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMCLibrary;
using PMCLibrary.DataContainers.Points;
using PMCLibrary.DataContainers.Positions;

namespace PMCUnitTests
{
    [TestClass]
    public class PositionTest
    {
        private const DimensionEnum Dimension = DimensionEnum._1D;
        private const int CountOfPoints = 3;

        private readonly Position<int> _position1D;

        public PositionTest()
        {
            _position1D = PositionFactoryMethod
                .GetPositionContainer<int>(Dimension, CountOfPoints);
        }

        [TestMethod]
        public void InnerPropertiesTest()
        {
            Assert.AreEqual(_position1D.Dimension, Dimension, "Wrong dimension");
            Assert.AreEqual(_position1D.CountOfPoints, CountOfPoints, "Wrong count of points");
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddWrontPointTest()
        {
            var point = new Point2D<int>.Builder() { X = 10, Y = 20 };
            _position1D.AddPoint(point);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PositionLimitTest()
        {
            var point = new Point1D<int>.Builder() { X = 10 };

            for (var i = 0; i < CountOfPoints; i++)
            {
                _position1D.AddPoint(point);
            }
            _position1D.AddPoint(point);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void WrongIndexTest()
        {
            var point = _position1D[CountOfPoints];
        }
    }
}
