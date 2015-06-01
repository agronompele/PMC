using System;
using PMCLibrary.DataContainers.Points;

namespace PMCLibrary.DataContainers.Positions
{
    /// <summary>
    /// Position for storing of 2D Points
    /// </summary>
    /// <typeparam name="T">Any numerical type</typeparam>
    public class Position2D<T> : Position<T> where T : struct
    {
        public Position2D(int countOfPoints) : base(countOfPoints)
        {
        }

        public override DimensionEnum Dimension
        {
            get { return DimensionEnum._2D; }
        }

        public override void AddPoint(Point<T> point)
        {
            CheckIfLimitWasReached();

            if (point is Point2D<T>)
                Points.Add(point);
            else throw new InvalidOperationException("Wrong type of Point");
        }
    }
}
