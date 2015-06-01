using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PMCLibrary.DataContainers.Positions;

namespace PMCLibrary.DataContainers
{
    /// <summary>
    /// Matrix is an indexed collection of position data
    /// </summary>
    /// <typeparam name="T">Any numerical type</typeparam>
    public class Matrix<T> : IEnumerable<Position<T>> where T : struct
    {
        /// <summary>
        /// Dimension of Point that will be stored
        /// </summary>
        public DimensionEnum Dimension { get; private set; }
        /// <summary>
        /// Count of Positions that will be stored
        /// </summary>
        public int CountOfPositions { get; private set; }
        public Position<T>[] Positions { get; private set; }

        /// <summary>
        /// Checks Position according the rules
        /// </summary>
        /// <param name="inputPosition">Position that will be checked if it is acceptable according the rules</param>
        /// <returns></returns>
        private bool CheckIfPositionIsAcceptable(Position<T> inputPosition)
        {
            if (inputPosition.Dimension != DimensionEnum._3D) return true;

            foreach (var position in Positions.Where(position => position != null))
            {
                if (inputPosition.CountOfPoints != position.CountOfPoints)
                    return false;
                break;
            }
            return true;
        }
        public IEnumerator<Position<T>> GetEnumerator()
        {
            return ((IEnumerable<Position<T>>)Positions).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Position<T> this[int index]
        {
            get
            {
                if (Positions[index] == null)
                    throw new InvalidOperationException("Value hasn't been added yet");
                return Positions[index];
            }

            set
            {
                if (value.Dimension != Dimension)
                    throw new InvalidOperationException("Positions have different type");
                if (!CheckIfPositionIsAcceptable(value))
                    throw new InvalidOperationException("Position is not acceptable in this matrix");

                Positions[index] = value;
            }
        }

        public sealed class Builder : Builder<Matrix<T>>
        {
            private readonly Matrix<T> _instance = new Matrix<T>();

            protected override Matrix<T> GetInstance()
            {
                return _instance;
            }

            public DimensionEnum Dimension
            {
                get { return _instance.Dimension; }
                set { _instance.Dimension = value; }
            }

            public int CountOfPositions
            {
                get { return _instance.CountOfPositions; }
                set
                {
                    _instance.CountOfPositions = value;
                    _instance.Positions = new Position<T>[CountOfPositions];
                }
            }
        }
    }
}
