using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMCLibrary;
using PMCLibrary.DataContainers;
using PMCLibrary.DataContainers.Positions;

namespace PMCUnitTests
{
    [TestClass]
    public class ContainerCollectionTest
    {
        private ContainerCollection<int> _containerCollection; 

        private readonly Container<int> _container1;
        private readonly Container<int> _container2;
        private readonly Container<int> _container3;
        private readonly Container<int> _container4;

        private readonly Matrix<int> _matrix1;
        private readonly Matrix<int> _matrix2;
        private readonly Matrix<int> _matrix3;

        private readonly Position<int> _position1;
        private readonly Position<int> _position2;

        public ContainerCollectionTest()
        {
            _containerCollection = new ContainerCollection<int>.Builder()
            {
                CountOfContainers = 4,
                CountOfMatrices = 2,
                CountOfPositions = 2
            };

            _container1 = new Container<int>.Builder()
            {
                CountOfMatrices = 2,
                CountOfPositions = 2
            };

            _container2 = new Container<int>.Builder()
            {
                CountOfMatrices = 2,
                CountOfPositions = 3
            };

            _container3 = new Container<int>.Builder()
            {
                CountOfMatrices = 3,
                CountOfPositions = 2
            };

            _container4 = new Container<int>.Builder()
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
                CountOfPositions = 2,
                Dimension = DimensionEnum._2D
            };

            _matrix3 = new Matrix<int>.Builder()
            {
                CountOfPositions = 2,
                Dimension = DimensionEnum._3D
            };

            _position1 = PositionFactoryMethod
               .GetPositionContainer<int>(DimensionEnum._3D, 2);

            _position2 = PositionFactoryMethod
                .GetPositionContainer<int>(DimensionEnum._3D, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MatrixWasNotAddedYetTest()
        {
            var container = _containerCollection[0];
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void WrongPositionCountTest()
        {
            _containerCollection[0] = _container1;
            _containerCollection[1] = _container2;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WrongMatrixCountTest()
        {
            _containerCollection = new ContainerCollection<int>.Builder()
            {
                CountOfContainers = 4,
                CountOfMatrices = 2,
                CountOfPositions = 2
            };

            _containerCollection[0] = _container1;
            _containerCollection[1] = _container3;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WrongMatrixTypeAccrossContainersTest()
        {
            _containerCollection = new ContainerCollection<int>.Builder()
            {
                CountOfContainers = 4,
                CountOfMatrices = 2,
                CountOfPositions = 2
            };

            _container1[0] = _matrix1;
            _container1[1] = _matrix2;

            _container4[0] = _matrix2;
            _container4[1] = _matrix1;

            _containerCollection[0] = _container1;
            _containerCollection[1] = _container4;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WrongPointCountInXyzMatrixTest()
        {
            _containerCollection = new ContainerCollection<int>.Builder()
            {
                CountOfContainers = 4,
                CountOfMatrices = 2,
                CountOfPositions = 2
            };

            _matrix1[0] = _position1;
            _matrix1[1] = _position1;

            _matrix3[0] = _position2;
            _matrix3[1] = _position2;

            _container1[0] = _matrix1;
            _container1[1] = _matrix3;

            _container4[0] = _matrix3;
            _container4[1] = _matrix1;

            _containerCollection[0] = _container1;
            _containerCollection[1] = _container4;
        }
    }
}
