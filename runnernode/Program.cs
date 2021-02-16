using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace runnernode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://167.172.19.100/tracker");
                Player payload = new Player();
                payload.Id = Environment.MachineName;
                HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, "");
                msg.Headers.Clear();

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = false,
                };
                var jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(payload, options);
                msg.Content = new ByteArrayContent(jsonUtf8Bytes);
                msg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.SendAsync(msg);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nValid!");
                }
                else
                {
                    Console.WriteLine("\nException Caught!");
                }
            }
        }
    }
}
