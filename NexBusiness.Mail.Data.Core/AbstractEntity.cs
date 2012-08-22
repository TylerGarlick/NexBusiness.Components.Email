using System;

namespace NexBusiness.Mail.Data.Core
{
    public abstract class AbstractEntity
    {
        protected AbstractEntity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}