using NexBusiness.Mail.Data.Core;
using System;
using System.Collections.Generic;
using NexBusiness.Repositories.RavenDb.Common;

namespace NexBusiness.Mail.Repositories.Common
{
    public interface IEmailTemplateRepository : IRepository<EmailTemplate>
    {
        IEnumerable<EmailTemplate> AllByRealm(Guid key);
    }
}
