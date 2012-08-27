using System;
using System.Collections.Generic;
using NexBusiness.Mail.Data.Core;

namespace NexBusiness.Mail.Services.Common
{
    public interface IEmailTemplateService
    {
        IEnumerable<EmailTemplate> GetAllEmailTemplates(Guid realmToken);
        EmailTemplate GetEmailTemplateById(Guid realmToken, string id);
        EmailTemplate GetEmailTemplateBySubject(Guid realmToken, string subject);
        EmailTemplate Save(Guid realmToken, EmailTemplate emailTemplate);
        void Delete(Guid realmToken, string id);
    }
}