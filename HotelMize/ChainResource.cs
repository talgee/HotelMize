
namespace HotelMize
{
    public class ChainResource<T> : IChainResource<T>
    {
        private IReadStorage<T>[] storages;

        public ChainResource(IReadStorage<T>[] storages)
        {
            this.storages = storages;
        }

        public async Task<T> GetValue()
        {
            T? value = default;

            var storagesToReWrite = new List<IReadWriteStorage<T>>();

            for (int i = 0; i <= storages.Length; i++) 
            {
                var currStorage = storages[i];
                
                if (currStorage is IReadWriteStorage<T>) 
                {
                    var readWriteStorage = (IReadWriteStorage<T>)currStorage;

                    if (readWriteStorage.ExpirationInterval.Ticks < DateTime.Now.Ticks) 
                    {
                        storagesToReWrite.Add(readWriteStorage);
                        continue;
                    }
                }

                try
                { 
                    value = await currStorage.ReadValue();

                    break;
                }
                catch  
                {
                    continue;
                }
            }

            if (value != null && !value.Equals(default(T)))
            {
                foreach (var storage in storagesToReWrite)
                {
                    await storage.WriteValue(value);
                }
            }

            return value;
        }
    }
}
