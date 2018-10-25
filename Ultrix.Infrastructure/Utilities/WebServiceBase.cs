using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ultrix.Infrastructure.Utilities
{
    public class WebServiceBase
    {
        private HttpClient Client { get; set; }

        public WebServiceBase(Uri baseAddress)
        {
            Client = new HttpClient { BaseAddress = baseAddress };
        }

        public async Task<string> GetAsync(string path)
        {
            try
            {
                HttpResponseMessage responseMessage = await Client.GetAsync(path);
                responseMessage.EnsureSuccessStatusCode();
                string stringResult = await responseMessage.Content.ReadAsStringAsync();
                return stringResult;
            }
            catch (HttpRequestException httpRequestException)
            {
                throw httpRequestException;
            }
        }
        public async Task<T> GetJsonAsyncAndConvertTo<T>(string path)
        {
            try
            {
                string stringResult = await GetAsync(path);
                return JsonConvert.DeserializeObject<T>(stringResult);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public async Task PostAsync(string path, params KeyValuePair<string, string>[] postValues)
        {
            try
            {
                HttpContent httpContent = new FormUrlEncodedContent(postValues);
                HttpResponseMessage responseMessage = await Client.PostAsync(path, httpContent);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException httpRequestException)
            {
                throw httpRequestException;
            }
        }
    }
}
