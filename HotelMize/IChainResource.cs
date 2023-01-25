
namespace HotelMize
{
    public interface IChainResource<T>
    {
        Task<T> GetValue();
    }
}
