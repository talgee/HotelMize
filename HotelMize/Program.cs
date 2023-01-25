
namespace HotelMize
{
    class Program
    {
        static void Main(string[] args)
        {
            var storages = new IReadStorage<ExchangeRateList>[]
            {
                new MemoryStorage<ExchangeRateList>(),
                new FileSystemStorage<ExchangeRateList>("exchangeRates.json"),
                new WebServiceStorage<ExchangeRateList>(new Uri("https://openexchangerates.org/api/latest.json"))
            };

            var exchangeRateListResource = new ChainResource<ExchangeRateList>(storages);

            var exchangeRates = exchangeRateListResource.GetValue();
        }
    }
}