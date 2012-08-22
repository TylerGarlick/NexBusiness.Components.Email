using NexBusiness.Mail.Data.Core;
using NexBusiness.Mail.Repositories.Common;
using NexBusiness.Repositories.RavenDb.Core;
using Raven.Client;

namespace NexBusiness.Mail.Repositories.RavenDb
{
    public class EmailRepository : AbstractRavenRepository<Email>, IEmailRepository
    {
        public EmailRepository(IDocumentStore documentStore)
            : base(documentStore)
        {
        }
    }
}