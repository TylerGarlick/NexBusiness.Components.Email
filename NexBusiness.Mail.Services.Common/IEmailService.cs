using System.Collections.Generic;
using NexBusiness.Mail.Data.Core;

namespace NexBusiness.Mail.Services.Common
{
    public interface IEmailService
    {
        IEnumerable<Email> GetAllEmails();
        Email GetEmailById(string id);
        Email Save(Email email);
        void Delete(string id);
    }
}