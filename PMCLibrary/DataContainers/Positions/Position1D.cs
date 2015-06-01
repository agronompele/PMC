using System;
using PMCLibrary.DataContainers.Points;

namespace PMCLibrary.DataContainers.Positions
{
    /// <summary>
    /// Position for storing of 1D Points
    /// </summary>
    /// <typeparam name="T">Any numerical type</typeparam>
    public class Position1D<T> : Position<T> where T : struct
    {
        public Position1D(int countOfPoints) : base(countOfPoints)
        {
        }

        public override DimensionEnum Dimension
        {
            get { return DimensionEnum._1D; }
        }

        public override void AddPoint(Point<T> point)
        {
            CheckIfLimitWasReached();

            if (point is Point1D<T>)
                Points.Add(point);
            else throw new InvalidOperationException("Wrong type of Point");
        }
    }
}
