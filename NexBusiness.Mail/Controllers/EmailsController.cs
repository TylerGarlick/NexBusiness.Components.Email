using AutoMapper;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using NexBusiness.Mail.Models;
using NexBusiness.Mail.Services.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NexBusiness.Mail.Controllers
{
    public class EmailsController : ApiController
    {
        const string QueueName = "nexbusiness.mail";

        protected IEmailService EmailService { get; private set; }
        protected IEmailTemplateService EmailTemplateService { get; private set; }
        protected IRealmService RealmService { get; private set; }
        protected QueueClient QueueClient { get; private set; }

        public EmailsController(IEmailService emailService, IEmailTemplateService emailTemplateService, IRealmService realmService)
        {
            EmailService = emailService;
            EmailTemplateService = emailTemplateService;
            RealmService = realmService;

            var connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            var namespaceManager = NamespaceManager.CreateFromConnectionString(ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"]);
            if (!namespaceManager.QueueExists(QueueName))
                namespaceManager.CreateQueue(QueueName);

            QueueClient = QueueClient.CreateFromConnectionString(connectionString, QueueName);

        }

        public IEnumerable<Email> Get(Guid realmKey, [FromUri]string emailTemplateId)
        {
            var realm = RealmService.GetRealmByKey(realmKey);
            if (realm == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Realm not found.")));

            var emailTemplate = EmailTemplateService.GetAllEmailTemplates(realmKey).FirstOrDefault(e => e.Id == emailTemplateId);
            if (emailTemplate == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Email Template with {0} was not found.", emailTemplateId)));

            var emails = emailTemplate.Emails;
            return Mapper.DynamicMap<IEnumerable<Email>>(emails);
        }

        public Email Get(Guid realmKey, [FromUri]string emailTemplateId, string id)
        {
            var realm = RealmService.GetRealmByKey(realmKey);
            if (realm == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Realm not found.")));

            var emailTemplate = EmailTemplateService.GetAllEmailTemplates(realmKey).FirstOrDefault(e => e.Id == emailTemplateId);
            if (emailTemplate == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Email Template with {0} was not found.", emailTemplateId)));

            var email = emailTemplate.Emails.FirstOrDefault(e => e.Id == id);

            return Mapper.DynamicMap<Email>(email);
        }

        public HttpResponseMessage Post(Guid realmKey, [FromUri]string emailTemplateId, Email model)
        {
            var realm = RealmService.GetRealmByKey(realmKey);
            if (realm == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Realm not found.")));

            var emailTemplate = EmailTemplateService.GetAllEmailTemplates(realmKey).FirstOrDefault(e => e.Id == emailTemplateId);
            if (emailTemplate == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Email Template with {0} was not found.", emailTemplateId)));

            var email = new Data.Core.Email();

            foreach (var key in model.Fields.Keys)
                email.Fields[key] = model.Fields[key];

            foreach (var recipient in model.Recipients)
                email.Recipients.Add(new Data.Core.Recipient() {Email = recipient.Email, Name = recipient.Name});

            email = EmailService.Save(email);

            emailTemplate.Emails.Add(email);
            emailTemplate = EmailTemplateService.Save(realmKey, emailTemplate);

            SendMessageToAzure(realmKey, emailTemplate, email);

            return Request.CreateResponse(HttpStatusCode.OK, email);
        }

        void SendMessageToAzure(Guid realmKey, Data.Core.EmailTemplate emailTemplate, Data.Core.Email email)
        {
            var message = new BrokeredMessage();

            message.Properties["EmailId"] = email.Id;
            message.Properties["EmailTemplateId"] = emailTemplate.Id;
            message.Properties["RealmToken"] = realmKey;

            QueueClient.Send(message);
        }
    }
}
