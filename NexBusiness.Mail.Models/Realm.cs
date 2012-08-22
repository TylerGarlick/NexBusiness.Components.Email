using System;
using System.Collections.Generic;

namespace NexBusiness.Mail.Models
{
    public class Realm : BasicRealm
    {
        public List<EmailTemplate> EmailTemplates { get; set; }
    }

    public class BasicRealm
    {
        public string Id { get; set; }
        public Guid Key { get; set; }
    }
}
