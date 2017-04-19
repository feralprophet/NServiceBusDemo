using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Billing.Api.IoC;
using Billing.Api.Service;
using Billing.Api.Test;
using Funq;
using Microsoft.Practices.Unity;
using NServiceBus;
using NServiceBus.Container;
using NServiceBus.Logging;
using ServiceStack;

namespace Billing.Api
{
    public class AppHost: AppHostBase
    {
        public AppHost() : base("Billing.Api", typeof(PolicyTermService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            var unityContainer = new UnityContainer();
            ConfigureDi(unityContainer);
            container.Adapter = new UnityIocAdapter(unityContainer);
            ConfigureMessageEndpoint(unityContainer);
        }

        private void ConfigureMessageEndpoint(IUnityContainer container)
        {
            
            var endpointConfiguration = new EndpointConfiguration("Billing.Api");
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.PurgeOnStartup(true);
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.RegisterComponents(configuredComponents =>
            {
                configuredComponents.ConfigureComponent<PolicyTermService>(DependencyLifecycle.InstancePerCall);
            });
            endpointConfiguration.UseContainer<UnityBuilder>(customizations =>
            {
                customizations.UseExistingContainer(container);
            });
           
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            LogManager.Use<NLogFactory>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.SendFailedMessagesTo("error");

            var endpoint = NServiceBus.Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            container.RegisterInstance(endpoint);
        }

        private void ConfigureDi(IUnityContainer container)
        {
            container.RegisterType<ITest, Test.TestClass>();
        }
    }
}