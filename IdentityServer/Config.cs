using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;


namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            List<Client> clients = new List<Client>();

            clients.Add(new Client()
            {
                ClientId = "Client1",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = {"api1"}
            });

            return clients;
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            List<ApiResource> apiResources = new List<ApiResource>();
            apiResources.Add(new ApiResource("api1", "First API"));
            return apiResources;
        }

        //Defining the InMemory User's
        public static List<TestUser> GetUsers()
        {
            List<TestUser> testUsers = new List<TestUser>();

            testUsers.Add(new TestUser()
            {
                SubjectId= "1",
                Username = "peter",
                Password = "password"
            });

            return testUsers;
        }
    }

   
}




