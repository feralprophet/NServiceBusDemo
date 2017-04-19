using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Messages.Billing;
using NServiceBus;

namespace Billing.Service.Handlers
{
    public class CreatePolicyTermHandler: IHandleMessages<CreateTermCommand>
    {
        public async Task Handle(CreateTermCommand message, IMessageHandlerContext context)
        {
            //todo: do some work
            Console.WriteLine($"We just created {message.PolicyNumber}");

            await context.Publish<TermCreatedEvent>(e =>
            {
                e.PolicyNumber = message.PolicyNumber;
            });
        }
    }
}
