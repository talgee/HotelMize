
namespace HotelMize
{
    internal interface IReadWriteStorage<T> : IReadStorage<T>
    {
        Task WriteValue(T value);
        TimeSpan ExpirationInterval { get; }
    }
}
