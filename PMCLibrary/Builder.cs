namespace PMCLibrary
{
    /// <summary>
    /// Base class for Builder design pattern realization
    /// </summary>
    /// <typeparam name="T">Any numerical type</typeparam>
    public abstract class Builder<T>
    {
        public static implicit operator T(Builder<T> builder)
        {
            return builder.Build();
        }

        public T Build()
        {
            return GetInstance();
        }

        protected abstract T GetInstance();
    }
}
