using System;
using System.Collections;
using System.Collections.Generic;

namespace PMCLibrary.DataContainers
{
    /// <summary>
    /// Container is an indexed collection of matrices
    /// </summary>
    /// <typeparam name="T">Any numerical type</typeparam>
    public class Container<T> : IEnumerable<Matrix<T>> where T : struct
    {
        /// <summary>
        /// Count of Matrices that will be stored
        /// </summary>
        public int CountOfMatrices { get; private set; }
        /// <summary>
        /// Count of Positions that will be stored in every Matrix
        /// </summary>
        public int CountOfPositions { get; private set; }

        /// <summary>
        /// Checks Matrix according the rules
        /// </summary>
        /// <param name="matrix">Matrix that will be checked if it is acceptable according the rules</param>
        /// <returns></returns>
        private bool CheckIfMatrixIsAcceptable(Matrix<T> matrix)
        {
            return matrix.CountOfPositions == CountOfPositions;
        }

        public IEnumerator<Matrix<T>> GetEnumerator()
        {
            return ((IEnumerable<Matrix<T>>)Matrices).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Matrix<T>[] Matrices { get; private set; }

        public Matrix<T> this[int index]
        {
            get
            {
                if (Matrices[index] == null)
                    throw new InvalidOperationException("Value hasn't been added yet");
                return Matrices[index];
            }

            set
            {
                if(!CheckIfMatrixIsAcceptable(value))
                    throw new InvalidOperationException("Invalid count of positions");

                Matrices[index] = value;
            }
        }
        public sealed class Builder : Builder<Container<T>>
        {
            private readonly Container<T> _instance = new Container<T>();

            protected override Container<T> GetInstance()
            {
                return _instance;
            }

            public int CountOfMatrices
            {
                get { return _instance.CountOfMatrices; }
                set
                {
                    _instance.CountOfMatrices = value;
                    _instance.Matrices = new Matrix<T>[CountOfMatrices];
                }
            }

            public int CountOfPositions
            {
                get { return _instance.CountOfPositions; }
                set
                {
                    _instance.CountOfPositions = value;
                }
            }
        }
    }

}
