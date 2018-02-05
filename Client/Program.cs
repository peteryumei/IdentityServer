using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            //Console.WriteLine("Hello World!");
            var identityServer = await DiscoveryClient.GetAsync("http://localhost:61086"); //discover the IdentityServer
            if (identityServer.IsError)
            {
                Console.Write(identityServer.Error);
                return;
            }

            //Get the token
            var tokenClient = new TokenClient(identityServer.TokenEndpoint, "Client1", "secret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("peter","password", "api1");

            //Call the API
            HttpClient client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);


            var response = await client.GetAsync("http://localhost:61362/api/values");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));
            Console.ReadKey();
        }
    }
}
