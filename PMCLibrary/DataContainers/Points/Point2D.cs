namespace PMCLibrary.DataContainers.Points
{
    /// <summary>
    /// 2D Point
    /// </summary>
    /// <typeparam name="T">Any numerical type</typeparam>
    public class Point2D<T> : Point<T> where T : struct
    {
        public T Y { get; private set; }

        public override string ToString()
        {
            return string.Format("X = {0}, Y = {1}", X, Y);
        }
        public sealed class Builder : Builder<Point2D<T>>
        {
            private readonly Point2D<T> _instance = new Point2D<T>();

            protected override Point2D<T> GetInstance()
            {
                return _instance;
            }

            public T X
            {
                get { return _instance.X; }
                set { _instance.X = value; }
            }

            public T Y
            {
                get { return _instance.Y; }
                set { _instance.Y = value; }
            }
        }
    }
}
