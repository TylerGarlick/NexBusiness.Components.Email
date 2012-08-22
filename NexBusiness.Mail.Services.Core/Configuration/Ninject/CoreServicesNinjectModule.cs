using NexBusiness.Mail.Repositories.RavenDb.Configuration.Ninject;
using NexBusiness.Mail.Services.Common;
using Ninject.Modules;

namespace NexBusiness.Mail.Services.Core.Configuration.Ninject
{
    public class CoreServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load(new[] { new RepositoryNinjectModule() });
            
            Bind<IEmailService>().To<EmailService>();
            Bind<IEmailTemplateService>().To<EmailTemplateService>();
            Bind<IRealmService>().To<RealmService>();
        }
    }
}
