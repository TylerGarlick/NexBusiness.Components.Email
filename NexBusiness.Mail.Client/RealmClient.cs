using System;
using System.Net.Http;
using NexBusiness.Mail.Models;

namespace NexBusiness.Mail.Client.Core
{
    public class RealmClient : AbstractClient, IRealmClient
    {
        const string RealmUrl = "Realms";

        public RealmClient(string baseUrl)
            : base(baseUrl)
        {
        }

        public ClientResponse<BasicRealm> CreateRealm(string name)
        {
            var clientResponse = new ClientResponse<BasicRealm>();
            var response = HttpClient.PostAsJsonAsync(RealmUrl, name).Result;
            
            clientResponse.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
                clientResponse.Result = response.Content.ReadAsAsync<BasicRealm>().Result;
            else
                clientResponse.Message = response.Content.ReadAsStringAsync().Result;


            return clientResponse;
        }
    }
}
