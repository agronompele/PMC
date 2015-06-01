using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMCLibrary;
using PMCLibrary.DataContainers;

namespace PMCUnitTests
{
    [TestClass]
    public class ContainerTest
    {
        private readonly Container<int> _container;
         
        private readonly Matrix<int> _matrix1;
        private readonly Matrix<int> _matrix2;

        public ContainerTest()
        {
            _container = new Container<int>.Builder()
            {
                CountOfMatrices = 2,
                CountOfPositions = 2
            };

            _matrix1 = new Matrix<int>.Builder()
            {
                CountOfPositions = 2,
                Dimension = DimensionEnum._3D
            };

            _matrix2 = new Matrix<int>.Builder()
            {
                CountOfPositions = 4,
                Dimension = DimensionEnum._2D
            };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MatrixWasNotAddedYetTest()
        {
            var matrix = _container[0];
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WrongPointCountInXyzMatrixTest()
        {
            _container[0] = _matrix1;
            _container[1] = _matrix2;
        }
    }
}
