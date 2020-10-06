using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace COVID_test
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            var repositories = await ProcessRepositories();

            foreach (var repo in repositories.Countries)
            {
                Console.WriteLine(repo.CountryStr);
                Console.WriteLine($"Total deaths: {repo.TotalDeaths}");
                Console.WriteLine($"New deaths: {repo.NewDeaths}");
                Console.WriteLine(repo.Date);
                Console.WriteLine("========================");
            }
        }

        private static async Task<Root> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            var streamTask = client.GetStreamAsync("https://api.covid19api.com/summary");

            var myDeserializedClass = await JsonSerializer.DeserializeAsync<Root>(await streamTask);

            return myDeserializedClass;
        }
    }
}
