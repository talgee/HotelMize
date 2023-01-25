
using System.Configuration;

namespace HotelMize
{
    public class MemoryStorage<T> : IReadWriteStorage<T>
    {
        private T? _value;
        private TimeSpan _expirationInterval;

        public TimeSpan ExpirationInterval { get { return _expirationInterval; } }

        public MemoryStorage()
        {
            ResetExpiration();
        }
        public Task<T> ReadValue()
        {
            return Task.FromResult(_value);
        }
        
        public async Task WriteValue(T value)
        {
            _value = value;

            ResetExpiration();

            await Task.CompletedTask;
        }

        private void ResetExpiration()
        {
            var expirationInterval = Convert.ToInt32(ConfigurationManager.AppSettings["MemoryInterval"]);

            _expirationInterval = DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(expirationInterval));
        }
    }
}
