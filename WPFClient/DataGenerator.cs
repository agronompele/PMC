using PMCLibrary;

namespace WPFClient
{
    static class DataGenerator
    {
        private static DimensionEnum GetDimensionFromString(string dimension)
        {
            switch (dimension)
            {
                case "1D":
                    return DimensionEnum._1D;

                case "2D":
                    return DimensionEnum._2D;

                case "3D":
                    return DimensionEnum._3D;

                default:
                    return DimensionEnum._1D;
            }
        }

        public static void GenerateContainerCollection(CreationParameters parameters)
        {
            var dimensionEnum = GetDimensionFromString(parameters.Dimension);

            switch (parameters.NumericType)
            {
                case "int":
                    DataFiller.GenerateContainerCollection<int>(parameters.CountOfContainers,
                        parameters.CountOfMatrices, parameters.CountOfPositions,
                        parameters.CountOfPoints, dimensionEnum);
                    break;

                case "double":
                    DataFiller.GenerateContainerCollection<double>(parameters.CountOfContainers,
                        parameters.CountOfMatrices, parameters.CountOfPositions,
                        parameters.CountOfPoints, dimensionEnum);
                    break;

                case "decimal":
                    DataFiller.GenerateContainerCollection<decimal>(parameters.CountOfContainers,
                        parameters.CountOfMatrices, parameters.CountOfPositions,
                        parameters.CountOfPoints, dimensionEnum);
                    break;
            }
        }
    }
}
