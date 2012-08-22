using System;
using NexBusiness.Mail.Data.Core;
using NexBusiness.Repositories.RavenDb.Common;

namespace NexBusiness.Mail.Repositories.Common
{
    public interface IRealmRepository : IRepository<Realm>
    {
        Realm ByKey(Guid key);
    }
}
