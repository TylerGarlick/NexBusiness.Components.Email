using System.ComponentModel.DataAnnotations;

namespace NexBusiness.Mail.Data.Core
{
    public class Recipient
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Name { get; set; }
    }
}