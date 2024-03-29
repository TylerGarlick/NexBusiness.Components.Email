﻿using System;
using System.Collections.Generic;

namespace NexBusiness.Mail.Data.Core
{
    public class Realm : AbstractEntity
    {
        public Realm()
        {
            Key = Guid.NewGuid();
            EmailTemplates = new List<EmailTemplate>();
        }

        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public Guid Key { get; set; }

        public List<EmailTemplate> EmailTemplates { get; set; }
    }
}
