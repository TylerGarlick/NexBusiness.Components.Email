using System.ComponentModel.DataAnnotations;

namespace NexBusiness.Mail.Models
{
    public class Recipient
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Name { get; set; }
    }
}