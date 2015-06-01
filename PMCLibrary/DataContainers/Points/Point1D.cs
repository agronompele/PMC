namespace PMCLibrary.DataContainers.Points
{
    /// <summary>
    /// 1D Point
    /// </summary>
    /// <typeparam name="T">Any numerical type</typeparam>
    public class Point1D<T> : Point<T> where T : struct
    {
        public sealed class Builder : Builder<Point1D<T>>
        {
            private readonly Point1D<T> _instance = new Point1D<T>();

            protected override Point1D<T> GetInstance()
            {
                return _instance;
            }
            public T X
            {
                get { return _instance.X; }
                set { _instance.X = value; }
            }
        }
    }
}
