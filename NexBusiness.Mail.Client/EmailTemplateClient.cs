using System;
using System.Collections.Generic;
using System.Net.Http;
using NexBusiness.Mail.Models;

namespace NexBusiness.Mail.Client.Core
{
    public class EmailTemplateClient : AbstractClient, IEmailTemplateClient
    {
        public Guid RealmToken { get; private set; }
        string ResourceUrl { get { return string.Format("{0}/EmailTemplates", RealmToken); } }

        public EmailTemplateClient(Guid realmToken, string baseUrl)
            : base(baseUrl)
        {
            RealmToken = realmToken;
        }

        public ClientResponse<IEnumerable<EmailTemplate>> All()
        {
            var clientResponse = new ClientResponse<IEnumerable<EmailTemplate>>();
            var response = HttpClient.GetAsync(ResourceUrl).Result;

            clientResponse.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
                clientResponse.Result = response.Content.ReadAsAsync<IEnumerable<EmailTemplate>>().Result;
            else
                clientResponse.Message = response.Content.ReadAsStringAsync().Result;

            return clientResponse;
        }

        public ClientResponse<EmailTemplate> GetById(string id)
        {
            var clientResponse = new ClientResponse<EmailTemplate>();
            var response = HttpClient.GetAsync(ResourceUrl + "/" + id).Result;

            clientResponse.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
                clientResponse.Result = response.Content.ReadAsAsync<EmailTemplate>().Result;
            else
                clientResponse.Message = response.Content.ReadAsStringAsync().Result;

            return clientResponse;
        }

        public ClientResponse<EmailTemplate> Create(EmailTemplate emailTemplate)
        {
            var clientResponse = new ClientResponse<EmailTemplate>();
            var response = HttpClient.PostAsJsonAsync(ResourceUrl, emailTemplate).Result;

            clientResponse.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
                clientResponse.Result = response.Content.ReadAsAsync<EmailTemplate>().Result;
            else
                clientResponse.Message = response.Content.ReadAsStringAsync().Result;

            return clientResponse;
        }

        public ClientResponse<EmailTemplate> Update(EmailTemplate emailTemplate)
        {
            var clientResponse = new ClientResponse<EmailTemplate>();
            var response = HttpClient.PutAsJsonAsync(ResourceUrl + "/" + emailTemplate.Id, emailTemplate).Result;

            clientResponse.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
                clientResponse.Result = response.Content.ReadAsAsync<EmailTemplate>().Result;
            else
                clientResponse.Message = response.Content.ReadAsStringAsync().Result;

            return clientResponse;
        }

        public ClientResponse<EmailTemplate> Delete(string id)
        {
            var clientResponse = new ClientResponse<EmailTemplate>();
            var response = HttpClient.DeleteAsync(ResourceUrl + "/" + id).Result;

            clientResponse.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
                clientResponse.Result = response.Content.ReadAsAsync<EmailTemplate>().Result;
            else
                clientResponse.Message = response.Content.ReadAsStringAsync().Result;

            return clientResponse;
        }
    }
}
