using System.Collections.Generic;
using System.Transactions;
using NexBusiness.Mail.Data.Core;
using NexBusiness.Mail.Repositories.Common;
using NexBusiness.Mail.Services.Common;

namespace NexBusiness.Mail.Services.Core
{
    public class EmailService : IEmailService
    {
        protected IEmailRepository EmailRepository { get; private set; }

        public EmailService(IEmailRepository emailRepository)
        {
            EmailRepository = emailRepository;
        }

        public IEnumerable<Email> GetAllEmails()
        {
            return EmailRepository.All();
        }

        public Email GetEmailById(string id)
        {
            return EmailRepository.ById(id);
        }

        public void Delete(string id)
        {
            using (var transaction = new TransactionScope())
            {
                EmailRepository.Delete(id);
                transaction.Complete();
            }
        }

        public Email Save(Email email)
        {
            using (var transaction = new TransactionScope())
            {
                if (string.IsNullOrEmpty(email.Id))
                    email = EmailRepository.Add(email);
                else
                    email = EmailRepository.Update(email);

                transaction.Complete();
                return email;
            }
        }
    }
}
