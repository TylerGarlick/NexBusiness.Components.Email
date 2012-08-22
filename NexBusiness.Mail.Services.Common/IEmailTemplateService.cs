using System;
using System.Collections.Generic;
using NexBusiness.Mail.Data.Core;

namespace NexBusiness.Mail.Services.Common
{
    public interface IEmailTemplateService
    {
        IEnumerable<EmailTemplate> GetAllEmailTemplates(Guid realmToken);
        EmailTemplate GetEmailTemplateById(Guid realmToken, string id);
        EmailTemplate Save(Guid realmToken, EmailTemplate emailTemplate);
        void Delete(Guid realmToken, string id);
    }
}