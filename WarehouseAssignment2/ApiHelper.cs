using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WarehouseAssignment2
{
    public static class ApiHelper
    {
        private const string API_URL = "http://localhost/api2.php"; // adjust path if in subfolder
        private static readonly HttpClient client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(15)
        };

        public static async Task<T> Post<T>(Dictionary<string, string> fields)
        {
            var content = new FormUrlEncodedContent(fields);
            var response = await client.PostAsync(API_URL, content);
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }

        public static async Task<T> PostMultipart<T>(MultipartFormDataContent content)
        {
            var response = await client.PostAsync(API_URL, content);
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }

        public static async Task<T> Get<T>(Dictionary<string, string> queryParams)
        {
            var query = "";
            foreach (var kv in queryParams)
                query += $"&{kv.Key}={Uri.EscapeDataString(kv.Value)}";
            var response = await client.GetAsync($"{API_URL}?{query.TrimStart('&')}");
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
