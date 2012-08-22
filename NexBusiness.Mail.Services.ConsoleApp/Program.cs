using NexBusiness.Mail.Data.Core;
using NexBusiness.Mail.Services.Common;
using NexBusiness.Mail.Services.Core.Configuration.Ninject;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NexBusiness.Mail.Services.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new[] { new CoreServicesNinjectModule() });
            var realmService = kernel.Get<IRealmService>();

            var realm = realmService.Save(new Realm()
                                  {
                                      Key = Guid.NewGuid(),
                                  });

            Console.WriteLine(realm.Key);
            Console.Beep();
            Console.ReadLine();
        }
    }
}
