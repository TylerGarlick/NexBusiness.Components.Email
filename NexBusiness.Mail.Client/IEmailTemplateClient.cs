using System.Collections.Generic;
using NexBusiness.Mail.Models;

namespace NexBusiness.Mail.Client.Core
{
    public interface IEmailTemplateClient
    {
        ClientResponse<IEnumerable<EmailTemplate>> All();
        ClientResponse<EmailTemplate> GetById(string id);
        ClientResponse<EmailTemplate> Create(EmailTemplate emailTemplate);
        ClientResponse<EmailTemplate> Update(EmailTemplate emailTemplate);
        ClientResponse<EmailTemplate> Delete(string id);
    }
}