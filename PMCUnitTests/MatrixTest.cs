using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMCLibrary;
using PMCLibrary.DataContainers;
using PMCLibrary.DataContainers.Positions;

namespace PMCUnitTests
{
    [TestClass]
    public class MatrixTest
    {
        private readonly Matrix<int> _matrix;
        private readonly Position<int> _position1;
        private readonly Position<int> _position2;
        private readonly Position<int> _position3;

        public MatrixTest()
        {
            _matrix = new Matrix<int>.Builder()
            {
                CountOfPositions = 2,
                Dimension = DimensionEnum._3D
            };

            _position1 = PositionFactoryMethod
                .GetPositionContainer<int>(DimensionEnum._3D, 2);

            _position2 = PositionFactoryMethod
                .GetPositionContainer<int>(DimensionEnum._2D, 2);

            _position3 = PositionFactoryMethod
               .GetPositionContainer<int>(DimensionEnum._3D, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PositionWasNotAddedYetTest()
        {
            var position = _matrix[0];
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WrongDimensionTest()
        {
            _matrix[0] = _position2;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WrongPointCountInXyzMatrixTest()
        {
            _matrix[0] = _position1;
            _matrix[1] = _position3;
        }

    }
}
