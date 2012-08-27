using System.Collections.Generic;
using NexBusiness.Mail.Models;

namespace NexBusiness.Mail.Client.Core
{
    public interface IEmailClient
    {
        ClientResponse<IEnumerable<Email>> All(string emailTemplateId);
        ClientResponse<Email> GetById(string emailTemplateId, string id);
        ClientResponse<Email> Create(string emailTemplateId, Email emailTemplate);
    }
}