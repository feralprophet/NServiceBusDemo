using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Billing.Api.ServiceDto.PolicyTerm;
using Billing.Api.Test;
using Common.Messages.Billing;
using NServiceBus;

namespace Billing.Api.Service
{
    public class PolicyTermService: ServiceStack.Service
    {
        private readonly ITest _test;
        private readonly IEndpointInstance _endpoint;

        public PolicyTermService(IEndpointInstance endpoint)
        {
            if (endpoint == null)
                throw new ArgumentNullException(nameof(endpoint));
            _endpoint = endpoint;
        }

        //public PolicyTermService(ITest test)
        //{
        //    if (test == null) throw new ArgumentNullException(nameof(test));
        //    _test = test;
        //}

        public async Task Post(CreatePolicyTermRequest request)
        {

            await _endpoint.Send("Billing.Service", new CreateTermCommand { PolicyNumber = request.PolicyNumber}).ConfigureAwait(false);

        }
    }
}