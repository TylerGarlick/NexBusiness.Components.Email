using System;
using System.Net;
using System.Net.Http;
namespace NexBusiness.Mail.Client.Core
{
    public abstract class AbstractClient
    {
        protected string BaseUrl { get; private set; }
        protected HttpClient HttpClient { get; private set; }

        protected AbstractClient(string baseUrl)
        {
            BaseUrl = baseUrl;
            HttpClient = new HttpClient()
                             {
                                 BaseAddress = new Uri(baseUrl)
                             };
        }
    }

    public class ClientResponse<T>
    {
        public T Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
