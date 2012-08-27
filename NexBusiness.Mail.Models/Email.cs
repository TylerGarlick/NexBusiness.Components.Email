using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NexBusiness.Mail.Models
{
    public class Email
    {
        public Email()
        {
            Recipients = new List<Recipient>();
            Fields = new Dictionary<string, string>();
        }

        public string Id { get; set; }

        [Required]
        public List<Recipient> Recipients { get; set; }

        [Required]
        public Dictionary<string, string> Fields { get; set; }  

        public string HtmlResult { get; set; }

        public DateTime? SentOn { get; set; }
    }
}