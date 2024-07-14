using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extentions
{
    public class HttpClientExtensions : HttpClient
    {
        public HttpClientExtensions(string baseApiUri, string token)
        {
            this.DefaultRequestHeaders.Accept.Clear();
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.Timeout = TimeSpan.FromSeconds(15);
            if (!string.IsNullOrEmpty(baseApiUri)) this.BaseAddress = new Uri(baseApiUri);
            if (!string.IsNullOrEmpty(token)) this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T?> Get<T>(string pathUri)
        {
            HttpResponseMessage response = await this.GetAsync(pathUri);
            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.BadRequest)
            {
                var resultData = await response.Content.ReadFromJsonAsync<T>();
                return resultData;
            }
            else throw new Exception(response.ReasonPhrase);
        }
        public async Task<T?> Post<T, TData>(string pathUri, TData data)
        {
            HttpResponseMessage response = await this.PostAsJsonAsync(pathUri, data);
            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.BadRequest)
            {
                var resultData = await response.Content.ReadFromJsonAsync<T>();
                return resultData;
            }
            else throw new Exception(response.ReasonPhrase);
        }
        public async Task<T?> Patch<T>(string pathUri, T data)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), pathUri);
            httpRequestMessage.Content = new StringContent(data?.ToString() ?? string.Empty, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await this.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                var resultData = await response.Content.ReadFromJsonAsync<T>();
                return resultData;
            }
            else throw new Exception(response.ReasonPhrase);
        }
    }
}
