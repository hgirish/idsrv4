using System;
using System.Net.Http;
using System.Threading.Tasks;
using Constants;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ConsoleClientCredentialsFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }
        static async Task  MainAsync(string[] args)
        {
            Console.Title = "Console Client Credentials Flow";

            var response = await RequestTokenAsync();
            response.Show();
            if (response.IsError) {
                Console.WriteLine($"Error occured: ${response.HttpErrorReason}");
                Console.WriteLine(response.Raw);
                return;
            }
           // Console.ReadLine();
            await CallServiceAsync(response.AccessToken);

            Console.WriteLine("Hello World!");
        }

        private static async Task CallServiceAsync(string token)
        {
            var baseAddress = Constants.Constants.SampleApi;
            Console.WriteLine($"baseAddress: {baseAddress}");

            var client = new HttpClient {
                BaseAddress = new Uri(baseAddress)
            };
            client.SetBearerToken(token);
            var response = await client.GetStringAsync("identity");

            Console.WriteLine(JArray.Parse(response));

   

        }

        private static async Task<TokenResponse> RequestTokenAsync()
        {
            var disco = await DiscoveryClient.GetAsync(Constants.Constants.Authority);
            if (disco.IsError) {
                throw new Exception(disco.Error);
            }

            var client = new TokenClient(
                disco.TokenEndpoint, "client", "secret");

            return await client.RequestClientCredentialsAsync("api1");
        }
    }
}
