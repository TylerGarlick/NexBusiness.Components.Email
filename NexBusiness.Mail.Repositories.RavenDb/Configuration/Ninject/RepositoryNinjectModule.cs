using NexBusiness.Mail.Repositories.Common;
using Ninject.Activation;
using Ninject.Modules;
using Raven.Client;
using Raven.Client.Document;

namespace NexBusiness.Mail.Repositories.RavenDb.Configuration.Ninject
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDocumentStore>().ToProvider<RavenDocumentStoreProvider>().InSingletonScope();

            Bind<IEmailRepository>().To<EmailRepository>().InThreadScope();
            Bind<IEmailTemplateRepository>().To<EmailTemplateRepository>().InThreadScope();
            Bind<IRealmRepository>().To<RealmRepository>().InThreadScope();

        }
    }

    public class RavenDocumentStoreProvider : Provider<IDocumentStore>
    {
        protected override IDocumentStore CreateInstance(IContext context)
        {
            var store = new DocumentStore
            {
                ConnectionStringName = "RavenDB",
                Conventions = new DocumentConvention() { IdentityPartsSeparator = "-" }
            };

            store.Initialize();
            return store;
        }
    }
}
