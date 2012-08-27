using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using NexBusiness.Mail.Data.Core;
using NexBusiness.Mail.Services.Common;

namespace NexBusiness.Mail.Worker
{
    public class ProcessEmail
    {
        protected IEmailService EmailService { get; private set; }
        protected IEmailTemplateService EmailTemplateService { get; private set; }

        public ProcessEmail(Guid realmToken, string emailTemplateId, string emailId, IEmailService emailService, IEmailTemplateService emailTemplateService)
        {
            EmailService = emailService;
            EmailTemplateService = emailTemplateService;
            var emailTemplate = EmailTemplateService.GetEmailTemplateById(realmToken, emailTemplateId);
            var email = EmailService.GetEmailById(emailId);

            if (email != null && emailTemplate != null)
            {
                var processedHtml = ReplaceFieldsInHtml(emailTemplate, email);
                SendEmail(processedHtml, email, emailTemplate);
            }
        }

        void SendEmail(string processedHtml, Email email, EmailTemplate emailTemplate)
        {
            var client = new SmtpClient("smtp.sendgrid.net", 587)
                             {
                                 UseDefaultCredentials = false,
                                 Credentials = new NetworkCredential("support@nexbusiness.com", "orange5")
                             };
            var from = new MailAddress(emailTemplate.SentFromEmail, emailTemplate.SentFromName);

            foreach (var recipient in email.Recipients)
            {
                var message = new MailMessage(from, new MailAddress(recipient.Email, recipient.Name))
                                  {
                                      Body = processedHtml,
                                      Subject = emailTemplate.Subject,
                                  };

                try
                {
                    client.Send(message);
                    email.SentOn = DateTime.UtcNow;
                    email.HtmlResult = processedHtml;
                    EmailService.Save(email);
                    
                    Trace.WriteLine(string.Format("{0}: Email sent to {1}", email.SentOn, recipient.Email));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("{0}: Failure.  Email was not sent to {1}", email.SentOn, recipient.Email));
                    Trace.WriteLine(ex.Message);
                }
            }
        }

        string ReplaceFieldsInHtml(EmailTemplate emailTemplate, Email email)
        {
            var html = emailTemplate.Html;
            foreach (var key in email.Fields.Keys)
                html = html.Replace("{{" + key + "}}", email.Fields[key]);
            return html;
        }
    }
}
