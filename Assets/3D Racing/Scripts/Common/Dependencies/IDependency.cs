namespace Racing
{
    /// <summary>
    /// Интерфейс зависимости
    /// </summary>
    public interface IDependency<T>
    {
        void Construct(T obj);
    }
}