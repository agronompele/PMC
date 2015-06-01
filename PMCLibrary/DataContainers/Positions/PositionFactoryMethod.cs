namespace PMCLibrary.DataContainers.Positions
{
    /// <summary>
    /// Factory method for retrieving special Position
    /// </summary>
    public class PositionFactoryMethod
    {
        /// <summary>
        /// Retrieves special Position
        /// </summary>
        /// <typeparam name="T">Any numerical type</typeparam>
        /// <param name="dimension">Dimension of Point that will be stored</param>
        /// <param name="countOfPoints">Count of Points that will be stored</param>
        /// <returns></returns>
        public static Position<T> GetPositionContainer<T>(DimensionEnum dimension,
            int countOfPoints) where T : struct
        {
            switch (dimension)
            {
                case DimensionEnum._1D:
                    return new Position1D<T>(countOfPoints);

                case DimensionEnum._2D:
                    return new Position2D<T>(countOfPoints);

                case DimensionEnum._3D:
                    return new Position3D<T>(countOfPoints);
            }
            return null;
        }
    }
}
