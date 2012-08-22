using System;
using System.Collections.Generic;
using NexBusiness.Mail.Data.Core;

namespace NexBusiness.Mail.Services.Common
{
    public interface IRealmService
    {
        IEnumerable<Realm> GetAllRealms();
        Realm GetRealmById(string id);
        Realm GetRealmByKey(Guid key);
        Realm Save(Realm relm);
        void Delete(string id);
    }
}