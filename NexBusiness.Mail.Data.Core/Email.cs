using System;
using System.Collections.Generic;

namespace NexBusiness.Mail.Data.Core
{
    public class Email : AbstractEntity
    {
        public Email()
        {
            Recipients = new List<Recipient>();
            Fields = new Dictionary<string, string>();
        }

        public string Id { get; set; }

        public List<Recipient> Recipients { get; set; }

        public Dictionary<string, string> Fields { get; set; }

        public string HtmlResult { get; set; }

        public DateTime? SentOn { get; set; }
    }
}