using System.Net.Http;
using System.Text;
using AutoTestRunner.Core.Repositories.Interfaces;
using AutoTestRunner.Core.Services.Interfaces;

namespace AutoTestRunner.Worker.Extensions
{
    public static class HttpClientExtension
    {
        public static HttpResponseMessage Post<T>(this HttpClient client, IJsonService jsonService, string uri, T obj)
        {
            return client.PostAsync(uri, new StringContent(jsonService.Serialize(obj), Encoding.UTF8, "application/json"))
                         .GetAwaiter()
                         .GetResult();
        }
    }

}