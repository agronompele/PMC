using PMCLibrary.DataContainers;
using PMCLibrary.DataContainers.Points;
using PMCLibrary.DataContainers.Positions;

namespace PMCLibrary
{
    /// <summary>
    /// Generates data
    /// </summary>
    public static class DataFiller
    {
        /// <summary>
        /// Generates Point
        /// </summary>
        /// <typeparam name="T">Any numerical type</typeparam>
        /// <param name="dimension">Dimension of Point</param>
        /// <returns>Point of particular type</returns>
        private static Point<T> GeneratePoint<T>(DimensionEnum dimension) where T : struct
        {
            switch (dimension)
            {
                case DimensionEnum._1D:
                    return new Point1D<T>.Builder();
                case DimensionEnum._2D:
                    return new Point2D<T>.Builder();
                case DimensionEnum._3D:
                    return new Point3D<T>.Builder();
            }
            return null;
        }
        /// <summary>
        /// Generates filled ContainerCollection with inner data containers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="countOfContainers">Count of Containers</param>
        /// <param name="countOfMatrices">Count of Matrices</param>
        /// <param name="countOfPositions">Count of Positions</param>
        /// <param name="countOfPoints">Count of Points</param>
        /// <param name="dimension">Dimension of Point</param>
        /// <returns></returns>
        public static ContainerCollection<T> GenerateContainerCollection<T>(int countOfContainers,
            int countOfMatrices,
            int countOfPositions,
            int countOfPoints,
            DimensionEnum dimension) where T : struct
        {
            ContainerCollection<T> containerCollection = new ContainerCollection<T>.Builder()
            {
                CountOfContainers = countOfContainers,
                CountOfMatrices = countOfMatrices,
                CountOfPositions = countOfPositions
            };

            //Containers
            for (var i = 0; i < containerCollection.CountOfContainers; i++)
            {
                var container = new Container<T>.Builder()
                {
                    CountOfMatrices = containerCollection.CountOfMatrices,
                    CountOfPositions = containerCollection.CountOfPositions
                };

                containerCollection[i] = container;
            }

            //Matrices
            for (var i = 0; i < containerCollection.CountOfContainers; i++)
            {
                for (var j = 0; j < containerCollection.CountOfMatrices; j++)
                {
                    var matrix = new Matrix<T>.Builder()
                    {
                        CountOfPositions = containerCollection.CountOfPositions,
                        Dimension = dimension
                    };

                    containerCollection[i][j] = matrix;
                }
            }

            //Positions
            foreach (var container in containerCollection.Containers)
            {
                foreach (var matrix in container.Matrices)
                {
                    for (var j = 0; j < matrix.CountOfPositions; j++)
                    {
                        var position = PositionFactoryMethod.GetPositionContainer<T>(dimension, countOfPoints);
                        matrix[j] = position;
                    }
                }
            }

            //Points
            foreach (var container in containerCollection.Containers)
            {
                foreach (var matrix in container.Matrices)
                {
                    foreach (var position in matrix.Positions)
                    {
                        for (var i = 0; i < position.CountOfPoints; i++)
                        {
                            position.AddPoint(GeneratePoint<T>(dimension));
                        }
                    }
                }
            }

            return containerCollection;
        }
    }
}
