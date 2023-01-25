using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace HotelMize
{
    internal class FileSystemStorage<T> : IReadWriteStorage<T>
    {
        private string _filePath;
        private TimeSpan _expirationInterval;

        public TimeSpan ExpirationInterval { get { return _expirationInterval; } }

        public FileSystemStorage(string filePath)
        {
            _filePath = filePath;
            ResetExpiration();
        }

        public async Task<T> ReadValue()
        {
            if (!File.Exists(_filePath))
            {
                return default(T);
            }

            using (var reader = new StreamReader(_filePath))
            {
                var json = await reader.ReadToEndAsync();
                
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task WriteValue(T value)
        {
            using (var writer = new StreamWriter(_filePath))
            {
                var json = JsonConvert.SerializeObject(value);

                ResetExpiration();

                await writer.WriteAsync(json);
            }
        }

        private void ResetExpiration()
        {
            var ExpirationInterval = Convert.ToInt32(ConfigurationManager.AppSettings["FileSystemInterval"]);

            _expirationInterval = DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(ExpirationInterval));
        }
    }
}
