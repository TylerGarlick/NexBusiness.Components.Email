using System;
using System.Collections.Generic;
using System.Transactions;
using NexBusiness.Mail.Data.Core;
using NexBusiness.Mail.Repositories.Common;
using NexBusiness.Mail.Services.Common;

namespace NexBusiness.Mail.Services.Core
{
    public class RealmService : IRealmService
    {
        protected IRealmRepository RealmRepository { get; private set; }

        public RealmService(IRealmRepository realmRepository)
        {
            RealmRepository = realmRepository;
        }

        public IEnumerable<Realm> GetAllRealms()
        {
            return RealmRepository.All();
        }

        public Realm GetRealmById(string id)
        {
            return RealmRepository.ById(id);
        }

        public Realm GetRealmByKey(Guid id)
        {
            return RealmRepository.ByKey(id);
        }

        public Realm Save(Realm realm)
        {
            using (var transaction = new TransactionScope())
            {
                if (string.IsNullOrEmpty(realm.Id))
                    realm = RealmRepository.Add(realm);
                else
                    realm = RealmRepository.Update(realm);
                transaction.Complete();
                return realm;
            }
        }

        public void Delete(string id)
        {
            using (var transaction = new TransactionScope())
            {
                RealmRepository.Delete(id);
                transaction.Complete();
            }
        }
    }
}