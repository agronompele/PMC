using System;
using System.Collections;
using System.Collections.Generic;
using PMCLibrary.DataContainers.Points;

namespace PMCLibrary.DataContainers.Positions
{
    /// <summary>
    /// Base class for all Positions
    /// </summary>
    /// <typeparam name="T">Any numerical type</typeparam>
    public abstract class Position<T> : IEnumerable<Point<T>> where T : struct
    {
        protected List<Point<T>> Points { get; set; }
        public int CountOfPoints { get; protected set; }

        protected Position(int countOfPoints)
        {
            CountOfPoints = countOfPoints;
            Points = new List<Point<T>>(countOfPoints);
        }
        /// <summary>
        /// Check if limit of Points was reached
        /// </summary>
        protected void CheckIfLimitWasReached()
        {
            if (Points.Count == CountOfPoints)
                throw new InvalidOperationException("Limit of points was reached");
        }
        /// <summary>
        /// Check if index is not wrong
        /// </summary>
        protected void CheckIndexBoundaries(int index)
        {
            if (index < 0 || index >= CountOfPoints)
                throw new IndexOutOfRangeException();

            if (index > Points.Count)
                throw new InvalidOperationException("Value hasn't been added yet");
        }
        public IEnumerator<Point<T>> GetEnumerator()
        {
            return Points.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Point<T> this[int index]
        {
            get
            {
                CheckIndexBoundaries(index);
                return Points[index];
            }
        }
        /// <summary>
        /// Dimension of Points that are stored in this Position
        /// </summary>
        public abstract DimensionEnum Dimension { get; }
        /// <summary>
        /// Add Point to Position
        /// </summary>
        public abstract void AddPoint(Point<T> point);
    }
}
