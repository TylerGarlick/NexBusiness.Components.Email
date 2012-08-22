using System;
using System.Collections.Generic;
using System.Linq;
using NexBusiness.Mail.Data.Core;
using NexBusiness.Mail.Repositories.Common;
using NexBusiness.Repositories.RavenDb.Core;
using Raven.Client;

namespace NexBusiness.Mail.Repositories.RavenDb
{
    public class EmailTemplateRepository : AbstractRavenRepository<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(IDocumentStore documentStore)
            : base(documentStore)
        {

        }

        public IEnumerable<EmailTemplate> AllByRealm(Guid key)
        {
            var realm = DocumentSession.Query<Realm>().FirstOrDefault(et => et.Key == key);
            return realm != null ?
                realm.EmailTemplates : null;
        }
    }
}