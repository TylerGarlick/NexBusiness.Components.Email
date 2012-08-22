using System;
using System.Linq;
using System.Linq.Expressions;
using NexBusiness.Mail.Data.Core;
using NexBusiness.Mail.Repositories.Common;
using NexBusiness.Repositories.RavenDb.Core;
using Raven.Client;

namespace NexBusiness.Mail.Repositories.RavenDb
{
    public class RealmRepository : AbstractRavenRepository<Realm>, IRealmRepository
    {
        public RealmRepository(IDocumentStore documentStore)
            : base(documentStore)
        {
        }

        public Realm ByKey(Guid key)
        {
            return All().FirstOrDefault(r => r.Key == key);
        }
    }
}