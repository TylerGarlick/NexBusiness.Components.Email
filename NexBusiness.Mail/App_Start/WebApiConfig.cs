using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace NexBusiness.Mail
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "EmailTemplatesBySubject",
                routeTemplate: "api/v1/{realmKey}/EmailTemplates/BySubject/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "EmailTemplates", action="GetBySubject" }
            );


            config.Routes.MapHttpRoute(
                name: "EmailTemplates",
                routeTemplate: "api/v1/{realmKey}/EmailTemplates/{id}",
                defaults: new { id = RouteParameter.Optional, controller="EmailTemplates" }
            );

            config.Routes.MapHttpRoute(
                name: "Emails",
                routeTemplate: "api/v1/{realmKey}/EmailTemplates/{emailTemplateId}/Emails/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "Emails" }
            );

            config.Routes.MapHttpRoute(
                name: "Realms",
                routeTemplate: "api/v1/Realms/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "Realms" }
            );
        }
    }
}
