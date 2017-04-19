using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Common.Messages.Billing
{
    public class TermCreatedEvent: IEvent
    {
        public string PolicyNumber { get; set; }
    }
}
