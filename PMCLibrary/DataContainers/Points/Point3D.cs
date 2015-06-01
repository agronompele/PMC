namespace PMCLibrary.DataContainers.Points
{
    /// <summary>
    /// 3D Point
    /// </summary>
    /// <typeparam name="T">Any numerical type</typeparam>
    public class Point3D<T> : Point<T> where T : struct
    {
        public T Y { get; private set; }
        public T Z { get; private set; }

        public override string ToString()
        {
            return string.Format("X = {0}, Y = {1}, Z = {2}", X, Y, Z);
        }
        public sealed class Builder : Builder<Point3D<T>>
        {
            private readonly Point3D<T> _instance = new Point3D<T>();

            protected override Point3D<T> GetInstance()
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

            public T Z
            {
                get { return _instance.Z; }
                set { _instance.Z = value; }
            }
        }
    }
}
