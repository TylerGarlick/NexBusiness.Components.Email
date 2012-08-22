using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NexBusiness.Mail.Models
{
    public class EmailTemplate
    {
        public EmailTemplate()
        {
            Fields = new List<string>();
            IsActive = true;
        }

        public string Id { get; set; }

        [Required, Display(Name = "From Name")]
        public string SentFromName { get; set; }

        [Required, Display(Name = "From Email"), DataType(DataType.EmailAddress)]
        public string SentFromEmail { get; set; }

        [Required, Display(Name = "Html Source"), DataType(DataType.EmailAddress)]
        public string Html { get; set; }

        public List<string> Fields { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}