using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PMCLibrary.DataContainers
{
    /// <summary>
    /// ContainerCollection is an indexed collection of Containers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ContainerCollection<T> : IEnumerable<Container<T>> where T : struct
    {
        /// <summary>
        /// Count of Containers that will be stored
        /// </summary>
        public int CountOfContainers { get; private set; }
        /// <summary>
        /// Count of Matrices that will be stored in every Container
        /// </summary>
        public int CountOfMatrices { get; private set; }
        /// <summary>
        /// Count of Positions that will be stored in every Position
        /// </summary>
        public int CountOfPositions { get; private set; }
        public Container<T>[] Containers { get; private set; }

        /// <summary>
        /// Checks Container according the rules
        /// </summary>
        /// <param name="container">Container that will be checked if it is acceptable according the rules</param>
        /// <returns></returns>
        private bool CheckIfContainerIsAcceptable(Container<T> container)
        {
            if (container.CountOfMatrices != CountOfMatrices
                || container.CountOfPositions != CountOfPositions)
                return false;

            return CheckMatrices(container);
        }
        private bool CheckIfCountOfPointsAreEqual(Matrix<T> standartMatrix, Matrix<T> checkMatrix)
        {
            //If Position of Matrix doesn't contain Points yet - return true
            try
            {
                return standartMatrix[0].CountOfPoints == checkMatrix[0].CountOfPoints;
            }
            catch (InvalidOperationException)
            {
                return true;
            }

        }
        private bool CheckIfMatricesTypesAreEqual(Matrix<T> standartMatrix, Matrix<T> checkMatrix)
        {
            return standartMatrix.Dimension == checkMatrix.Dimension;
        }
        /// <summary>
        /// Checks Container's Matrices if they are acceptable with other Containers' Matrices in ContainerCollection
        /// </summary>
        /// <param name="container">>Container that will be checked if his Matrices are acceptable</param>
        /// <returns></returns>
        private bool CheckMatrices(Container<T> container)
        {
            var standartContainer = Containers.FirstOrDefault(con => con != null);
            if (standartContainer == null) return true;

            for (var i = 0; i < container.Matrices.Length; i++)
            {
                if (standartContainer.Matrices[i] == null) continue;

                if (!CheckIfMatricesTypesAreEqual(standartContainer.Matrices[i],
                    container.Matrices[i]))
                    return false;

                if (standartContainer.Matrices[i].Dimension != DimensionEnum._3D) continue;

                if (!CheckIfCountOfPointsAreEqual(standartContainer.Matrices[i],
                    container.Matrices[i]))
                    return false;
            }
            return true;
        }
        public IEnumerator<Container<T>> GetEnumerator()
        {
            return ((IEnumerable<Container<T>>)Containers).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Container<T> this[int index]
        {
            get
            {
                if (Containers[index] == null)
                    throw new InvalidOperationException("Value hasn't been added yet");
                return Containers[index];
            }

            set
            {
                if (!CheckIfContainerIsAcceptable(value))
                    throw new InvalidOperationException("Container is not acceptable");

                Containers[index] = value;
            }
        }

        public sealed class Builder : Builder<ContainerCollection<T>>
        {
            private readonly ContainerCollection<T> _instance = new ContainerCollection<T>();

            protected override ContainerCollection<T> GetInstance()
            {
                return _instance;
            }

            public int CountOfContainers
            {
                get { return _instance.CountOfContainers; }
                set
                {
                    _instance.CountOfContainers = value;
                    _instance.Containers = new Container<T>[CountOfContainers];
                }
            }
            public int CountOfMatrices
            {
                get { return _instance.CountOfMatrices; }
                set
                {
                    _instance.CountOfMatrices = value;
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
