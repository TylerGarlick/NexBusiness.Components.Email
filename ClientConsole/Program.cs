using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NexBusiness.Mail.Client.Core;
using NexBusiness.Mail.Models;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var emailTemplateClient = new EmailTemplateClient(Guid.Parse("8bda3ead-f305-460e-8a09-b222878374b5"), "http://localhost:17616/api/v1/");
            //var response = emailTemplateClient.Create(new EmailTemplate()
            //                                {
            //                                    IsActive = true,
            //                                    SentFromName = "Utah's Own",
            //                                    SentFromEmail = "support@utahsown.org",
            //                                    Subject = "Utah's Own | Membership Questionaire",
            //                                    Fields = new List<string>() { "Url", "ApplicationKindName" },
            //                                    Html = "<!DOCTYPE html><html><body>Thank you for your interest in Utah’s Own Membership. The Utah’s Own Brand is known and appreciated in Utah as the credible local brand for agriculture and food products.<br /><br />As a {{ApplicationKindName}}, we want to ensure your product/company qualifies for Utah’s Own Membership. Please click on the following link and answer the questionnaire:<br /><br /><a href=\"{{Url}}\">Click Here</a><br /><br />If you have any questions, feel free to contact Tamra Watson via email: <a href=\"mailto:tamrawatson@utah.gov\">tamrawatson@utah.gov</a> or call 801-538-4913.<br /><br />Thank You,<br /><br /><br /><br />The Utah’s Own Team<br /><a href=\"mailto:jedchristenson@utah.gov\">Jed Christenson (jedchristenson@utah.gov)</a><br /><a href=\"mailto:sethwinterton@utah.gov\">Seth Winterton (sethwinterton@utah.gov)</a><br /><a href=\"mailto:tamrawatson@utah.gov\">Tamra Watson (tamrawatson@utah.gov)</a></body></html>"
            //                                });


            var emailClient = new EmailClient(Guid.Parse("8bda3ead-f305-460e-8a09-b222878374b5"), "http://localhost:17616/api/v1/");
            var response = emailClient.Create("EmailTemplates-129", new Email()
                                                         {
                                                             Fields = new Dictionary<string, string>()
                                                                          {
                                                                              {"Url", "Google"}
                                                                          },
                                                             Recipients = new List<Recipient>()
                                                                              {
                                                                                  new Recipient() {Email = "Tyler Garlick", Name= "tjgarlick@gmail.com"}
                                                                              }
                                                         });

            //var response = realmClient.CreateRealm("UtahsOwn");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine(response.Result.HtmlResult);
            }

            Console.ReadLine();
        }
    }
}
