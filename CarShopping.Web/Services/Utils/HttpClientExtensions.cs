﻿using System.Net.Http.Headers;
using System.Text.Json;

namespace CarShopping.Web.Services.Utils
{
    public static class HttpClientExtensions
    {
        public static MediaTypeHeaderValue ContentType = new MediaTypeHeaderValue("application/json");
        public static async Task<T> ReadContentAs<T>
            (this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Something went wrong calling the API: " +
                $"{response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(dataAsString
                , new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = ContentType;
            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = ContentType;
            return httpClient.PutAsync(url, content);
        }

        public static Task<HttpResponseMessage> DeleeAsync<T>(this HttpClient httpClient, string url)
        {
            return httpClient.DeleteAsync(url);
        }
    }
}
