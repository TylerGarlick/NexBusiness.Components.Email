using System;
using System.Collections.Generic;
using System.Net.Http;
using NexBusiness.Mail.Models;

namespace NexBusiness.Mail.Client.Core
{
    public class EmailClient : AbstractClient, IEmailClient
    {
        public Guid RealmToken { get; private set; }

        public EmailClient(Guid realmToken, string baseUrl)
            : base(baseUrl)
        {
            RealmToken = realmToken;
        }

        public ClientResponse<IEnumerable<Email>> All(string emailTemplateId)
        {
            var clientResponse = new ClientResponse<IEnumerable<Email>>();
            var response = HttpClient.GetAsync(string.Format("{0}/EmailTemplates/{1}/Emails", RealmToken, emailTemplateId)).Result;

            clientResponse.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
                clientResponse.Result = response.Content.ReadAsAsync<IEnumerable<Email>>().Result;
            else
                clientResponse.Message = response.Content.ReadAsStringAsync().Result;

            return clientResponse;
        }

        public ClientResponse<Email> GetById(string emailTemplateId, string id)
        {
            var clientResponse = new ClientResponse<Email>();
            var response = HttpClient.GetAsync(string.Format("{0}/EmailTemplates/{1}/Emails/{2}", RealmToken, emailTemplateId, id)).Result;

            clientResponse.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
                clientResponse.Result = response.Content.ReadAsAsync<Email>().Result;
            else
                clientResponse.Message = response.Content.ReadAsStringAsync().Result;

            return clientResponse;
        }

        public ClientResponse<Email> Create(string emailTemplateId, Email emailTemplate)
        {
            var clientResponse = new ClientResponse<Email>();
            var response = HttpClient.PostAsJsonAsync(string.Format("{0}/EmailTemplates/{1}/Emails", RealmToken, emailTemplateId), emailTemplate).Result;

            clientResponse.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
                clientResponse.Result = response.Content.ReadAsAsync<Email>().Result;
            else
                clientResponse.Message = response.Content.ReadAsStringAsync().Result;

            return clientResponse;
        }
    }
}