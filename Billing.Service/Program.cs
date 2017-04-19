using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NServiceBus;

namespace Billing.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "Billing.Service";

            var endpointConfig = new EndpointConfiguration("Billing.Service");
            endpointConfig.UseTransport<MsmqTransport>();
            endpointConfig.UsePersistence<InMemoryPersistence>();
            endpointConfig.EnableInstallers();
            endpointConfig.SendFailedMessagesTo("error");

            var endpointInstance = await Endpoint.Start(endpointConfig).ConfigureAwait(false);

            try
            {
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
            finally
            {

                await endpointInstance.Stop().ConfigureAwait(false);
            }

        }
    }
}
