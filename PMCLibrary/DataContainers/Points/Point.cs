namespace PMCLibrary.DataContainers.Points
{
    /// <summary>
    /// Base class for all Points with different dimension types
    /// </summary>
    /// <typeparam name="T">Any numerical type</typeparam>
    public abstract class Point<T> where T : struct
    {
        public T X { get; protected set; }

        public override string ToString()
        {
            return string.Format("X = {0}", X);
        }
    }
}
