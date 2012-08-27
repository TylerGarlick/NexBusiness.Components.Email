using NexBusiness.Mail.Helpers;
using NexBusiness.Mail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using NexBusiness.Mail.Services.Common;

namespace NexBusiness.Mail.Controllers
{
    [Validate]
    public class EmailTemplatesController : ApiController
    {
        protected IEmailTemplateService EmailTemplateService { get; private set; }
        protected IRealmService RealmService { get; private set; }

        public EmailTemplatesController(IEmailTemplateService emailTemplateService, IRealmService realmService)
        {
            EmailTemplateService = emailTemplateService;
            RealmService = realmService;
        }

        public IEnumerable<EmailTemplate> Get(Guid realmKey)
        {
            return Mapper.DynamicMap<IEnumerable<EmailTemplate>>(EmailTemplateService.GetAllEmailTemplates(realmKey));
        }

        public EmailTemplate Get([FromUri]Guid realmKey, string id)
        {

            var emailTemplate = EmailTemplateService.GetAllEmailTemplates(realmKey).FirstOrDefault(e => e.Id == id);
            if (emailTemplate != null)
                return Mapper.DynamicMap<EmailTemplate>(emailTemplate);

            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Email Template with {0} was not found.", id)));
        }

        public EmailTemplate GetBySubject([FromUri]Guid realmKey, string id)
        {
            var emailTemplate = EmailTemplateService.GetAllEmailTemplates(realmKey).FirstOrDefault(e => e.Subject.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            if (emailTemplate != null)
                return Mapper.DynamicMap<EmailTemplate>(emailTemplate);

            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Email Template with subject {0} was not found.", id)));
        }

        public HttpResponseMessage Post([FromUri]Guid realmKey, EmailTemplate model)
        {
            var realm = RealmService.GetRealmByKey(realmKey);
            if (realm == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Realm not found.")));

            var emailTemplate = Mapper.DynamicMap<Data.Core.EmailTemplate>(model);
            emailTemplate = EmailTemplateService.Save(realmKey, emailTemplate);
            return Request.CreateResponse(HttpStatusCode.OK, Mapper.DynamicMap<EmailTemplate>(emailTemplate));
        }

        public HttpResponseMessage Put([FromUri]Guid realmKey, string id, EmailTemplate model)
        {
            var realm = RealmService.GetRealmByKey(realmKey);
            if (realm == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Realm not found.")));

            var emailTemplate = EmailTemplateService.GetAllEmailTemplates(realmKey).FirstOrDefault(e => e.Id == id);
            if (emailTemplate == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Email Template with {0} was not found.", id)));

            emailTemplate = Mapper.DynamicMap<Data.Core.EmailTemplate>(model);
            emailTemplate = EmailTemplateService.Save(realmKey, emailTemplate);
            return Request.CreateResponse(HttpStatusCode.OK, Mapper.DynamicMap<EmailTemplate>(emailTemplate));
        }

        public void Delete([FromUri]Guid realmKey, string id)
        {
            var realm = RealmService.GetRealmByKey(realmKey);
            if (realm == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Realm not found.")));

            var emailTemplate = EmailTemplateService.GetAllEmailTemplates(realmKey).FirstOrDefault(e => e.Id == id);
            if (emailTemplate == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Email Template with {0} was not found.", id)));

            EmailTemplateService.Delete(realmKey, id);
        }
    }
}
