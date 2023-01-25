
namespace HotelMize
{
    public interface IReadStorage<T>
    {
        Task<T> ReadValue();
    }
}
