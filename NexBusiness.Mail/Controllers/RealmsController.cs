using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using NexBusiness.Mail.Models;
using NexBusiness.Mail.Services.Common;
using NexBusiness.Mail.Helpers;

namespace NexBusiness.Mail.Controllers
{
    [Validate]
    public class RealmsController : ApiController
    {
        protected IRealmService RealmService { get; private set; }

        public RealmsController(IRealmService realmService)
        {
            RealmService = realmService;
        }

        public BasicRealm Get(Guid id)
        {
            var realm = RealmService.GetRealmByKey(id);
            if (realm != null)
                return Mapper.DynamicMap<BasicRealm>(realm);

            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Realm with key {0} was not found", id)));
        }

        public BasicRealm Post([FromBody] string name)
        {
            var realm = new Data.Core.Realm()
                        {
                            Key = Guid.NewGuid(),
                            Name = name
                        };

            realm = RealmService.Save(realm);
            return Mapper.DynamicMap<BasicRealm>(realm);
        }
    }
}
