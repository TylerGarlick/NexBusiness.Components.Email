using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using NexBusiness.Mail.Services.Core.Configuration.Ninject;
using Ninject;
using Ninject.Modules;
using NexBusiness.Mail.Services.Common;

namespace NexBusiness.Mail.Worker
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        const string QueueName = "nexbusiness.mail";

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        QueueClient Client;
        bool IsStopped;


        public IKernel Kernel { get; set; }

        public override void Run()
        {
            while (!IsStopped)
            {
                try
                {
                    // Receive the message
                    BrokeredMessage receivedMessage = null;
                    receivedMessage = Client.Receive();

                    if (receivedMessage != null)
                    {
                        var emailId = (string)receivedMessage.Properties["EmailId"];
                        var emailTemplateId = (string)receivedMessage.Properties["EmailTemplateId"];
                        var realmToken = (Guid)receivedMessage.Properties["RealmToken"];
                        var processEmail = new ProcessEmail(realmToken, emailTemplateId, emailId, Kernel.Get<IEmailService>(), Kernel.Get<IEmailTemplateService>());
                        
                        Trace.WriteLine("Processing", receivedMessage.SequenceNumber.ToString());
                        receivedMessage.Complete();
                    }
                }
                catch (MessagingException e)
                {
                    if (!e.IsTransient)
                    {
                        Trace.WriteLine(e.Message);
                        throw;
                    }

                    Thread.Sleep(10000);
                }
                catch (OperationCanceledException e)
                {
                    if (!IsStopped)
                    {
                        Trace.WriteLine(e.Message);
                        throw;
                    }
                }
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Create the queue if it does not exist already
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            // Initialize the connection to Service Bus Queue
            Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);
            IsStopped = false;

            Kernel = new StandardKernel(new INinjectModule[] { new CoreServicesNinjectModule() });
            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            IsStopped = true;
            Client.Close();
            base.OnStop();
        }
    }
}
