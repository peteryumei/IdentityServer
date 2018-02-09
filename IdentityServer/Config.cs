using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
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

            //Client for MVC
            clients.Add(new Client()
            {
                ClientId = "mvc",
                ClientName = "MVC",
                AllowedGrantTypes = GrantTypes.Implicit,

                //where to redirect to after login
                RedirectUris = { "http://localhost:13998/signin-oidc" },

                //where to redirect to after Logout
                PostLogoutRedirectUris = { "http://localhost:13998/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles"
                },
                AlwaysIncludeUserClaimsInIdToken = true   //It should be true to send Claims with the token

            });

            return clients;
        }

        //Defining the InMemory API's
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
                Username = "admin",
                Password = "password",

                Claims = new List<Claim>
                {
                    new Claim("role", "admin")
                }
            });

            testUsers.Add(new TestUser()
            {
                SubjectId = "1",
                Username = "editor",
                Password = "password",

                Claims = new List<Claim>
                {
                    new Claim("role", "editor")
                }
            });



            return testUsers;
        }

        //Support for OpenId connectivity scopes
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            List<IdentityResource> resources = new List<IdentityResource>();

            resources.Add(new IdentityResources.OpenId());
            resources.Add(new IdentityResources.Profile());
            resources.Add(new IdentityResource("roles", new[] { "role" }));

            return resources;
        }
    }

   
}




