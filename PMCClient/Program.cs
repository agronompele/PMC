using System;
using System.Diagnostics;
using System.Linq;
using PMCLibrary;
using PMCLibrary.DataContainers;
using PMCLibrary.DataContainers.Points;
using PMCLibrary.DataContainers.Positions;

namespace PMCClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintData();

            Console.WriteLine();

            MakeBenchmark();
        }

        private static void MakeBenchmark()
        {
            const int countOfContainers = 200;
            const int countOfMatrices = 200;
            const int countOfPositions = 20;
            const int countOfPoints = 20;
            const DimensionEnum dimension = DimensionEnum._2D;

            Console.WriteLine("This is example of benchmark");
            Console.WriteLine("Count of containers: {0}\n" +
                              "Count of matrices: {1}\n" +
                              "Count of positions: {2}\n" +
                              "Count of points: {3}\n" +
                              "Type: {4}",
                              countOfContainers, countOfMatrices,
                              countOfPositions, countOfPoints, dimension);


            var sw = new Stopwatch();
            Console.WriteLine("Start: " + sw.Elapsed);
            sw.Start();

            var containers = DataFiller.GenerateContainerCollection<int>(countOfContainers, countOfMatrices,
                countOfPositions, countOfPoints, dimension);

            sw.Stop();
            Console.WriteLine("Finish: " + sw.Elapsed);
        }

        private static void PrintData()
        {
            Console.WriteLine("This is example of creating data containers" +
                              " and displaying data on the display");

            const int countOfContainers = 1;
            const int countOfMatrices = 1;
            const int countOfPositions = 2;
            const int countOfPoints = 2;
            const DimensionEnum dimension = DimensionEnum._3D;

            #region Points

            var point3D_1 = new Point3D<int>.Builder()
            {
                X = 100,
                Y = 200,
                Z = 300
            };

            var point3D_2 = new Point3D<int>.Builder()
            {
                X = 500,
                Y = 600,
                Z = 700
            };

            #endregion

            #region Positions

            var position3d_1 = PositionFactoryMethod.GetPositionContainer<int>(dimension, countOfPoints);
            position3d_1.AddPoint(point3D_1);
            position3d_1.AddPoint(point3D_2);

            var position3d_2 = PositionFactoryMethod.GetPositionContainer<int>(dimension, countOfPoints);
            position3d_2.AddPoint(point3D_2);
            position3d_2.AddPoint(point3D_1);

            #endregion

            #region Matrices

            var matrix1 = new Matrix<int>.Builder()
            {
                CountOfPositions = countOfPositions,
                Dimension = dimension
            }.Build();

            matrix1[0] = position3d_1;
            matrix1[1] = position3d_2;

            #endregion

            #region Containers

            var container1 = new Container<int>.Builder()
            {
                CountOfMatrices = countOfMatrices,
                CountOfPositions = countOfPositions
            }.Build();
            container1[0] = matrix1;

            #endregion

            #region ContainerCollection

            var containerCollection = new ContainerCollection<int>.Builder()
            {
                CountOfContainers = countOfContainers,
                CountOfMatrices = countOfMatrices,
                CountOfPositions = countOfPositions
            }.Build();

            containerCollection[0] = container1;

            #endregion

            foreach (var point in containerCollection
                .SelectMany(container => container
                    .SelectMany(matrix => matrix
                        .SelectMany(position => position))))
                Console.WriteLine(point);
        }
    }
}
