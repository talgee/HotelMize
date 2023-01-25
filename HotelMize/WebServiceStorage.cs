using Newtonsoft.Json;
using RestSharp;
using System.Configuration;

namespace HotelMize
{
    internal class WebServiceStorage<T> : IReadStorage<T>
    {
        private Uri endpoint;

        public WebServiceStorage(Uri endpoint)
        {
            this.endpoint = endpoint;
        }

        public async Task<T> ReadValue()
        {
            
            string ApiKey = ConfigurationManager.AppSettings["ApiKey"];

            using (var client = new RestClient(endpoint))
            {
                var request = new RestRequest();

                request.AddQueryParameter("app_id", ApiKey);

                var json = client.Execute(request);

                return JsonConvert.DeserializeObject<T>(json.Content);
            }
        }
    }
}
