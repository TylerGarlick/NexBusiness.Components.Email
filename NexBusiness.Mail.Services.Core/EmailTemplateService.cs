using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using NexBusiness.Mail.Data.Core;
using NexBusiness.Mail.Repositories.Common;
using NexBusiness.Mail.Services.Common;

namespace NexBusiness.Mail.Services.Core
{
    public class EmailTemplateService : IEmailTemplateService
    {
        protected IEmailTemplateRepository EmailTemplateRepository { get; private set; }
        protected IRealmRepository RealmRepository { get; private set; }

        public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository, IRealmRepository realmRepository)
        {
            EmailTemplateRepository = emailTemplateRepository;
            RealmRepository = realmRepository;
        }

        public IEnumerable<EmailTemplate> GetAllEmailTemplates(Guid realmToken)
        {
            var realm = RealmRepository.ByKey(realmToken);
            return realm.EmailTemplates;
        }

        public EmailTemplate GetEmailTemplateById(Guid realmToken, string id)
        {
            var realm = RealmRepository.ByKey(realmToken);
            if (realm != null)
                return realm.EmailTemplates.FirstOrDefault(e => e.Id == id);

            return null;
        }

        public EmailTemplate GetEmailTemplateBySubject(Guid realmToken, string subject)
        {
            var realm = RealmRepository.ByKey(realmToken);
            if (realm != null)
                return realm.EmailTemplates.FirstOrDefault(e => e.Subject.Equals(subject, StringComparison.InvariantCultureIgnoreCase));

            return null;
        }

        public EmailTemplate Save(Guid realmToken, EmailTemplate emailTemplate)
        {
            using (var transaction = new TransactionScope())
            {
                var realm = RealmRepository.ByKey(realmToken);
                if (string.IsNullOrEmpty(emailTemplate.Id))
                {
                    emailTemplate = EmailTemplateRepository.Add(emailTemplate);
                    realm.EmailTemplates.Add(emailTemplate);
                    RealmRepository.Update(realm);
                }
                else
                    emailTemplate = EmailTemplateRepository.Update(emailTemplate);

                transaction.Complete();
                return emailTemplate;
            }
        }

        public void Delete(Guid realmToken, string id)
        {
            using (var transaction = new TransactionScope())
            {
                var realm = RealmRepository.ByKey(realmToken);
                var emailTemplate = realm.EmailTemplates.FirstOrDefault(c => c.Id == id);
                if (emailTemplate != null)
                {
                    EmailTemplateRepository.Delete(id);
                    transaction.Complete();
                }
            }
        }

        public EmailTemplate GetEmailTemplateById(string id)
        {
            return EmailTemplateRepository.ById(id);
        }
    }
}