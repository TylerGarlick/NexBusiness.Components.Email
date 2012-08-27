using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NexBusiness.Mail.Data.Core
{
    public class EmailTemplate : AbstractEntity
    {
        public EmailTemplate()
        {
            Fields = new List<string>();
            IsActive = true;
            Emails = new List<Email>();
        }

        public string Id { get; set; }

        [Required, Display(Name = "From Name")]
        public string SentFromName { get; set; }

        [Required, Display(Name = "From Email"), DataType(DataType.EmailAddress)]
        public string SentFromEmail { get; set; }

        [Required, Display(Name = "Subject"), DataType(DataType.EmailAddress)]
        public string Subject { get; set; }

        [Required, Display(Name = "Html Source"), DataType(DataType.EmailAddress)]
        public string Html { get; set; }

        public List<string> Fields { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public List<Email> Emails { get; set; }
    }
}