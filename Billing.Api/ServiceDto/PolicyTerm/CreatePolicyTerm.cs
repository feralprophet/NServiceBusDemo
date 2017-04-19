using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace Billing.Api.ServiceDto.PolicyTerm
{
    [Route("/api/billing/policy", "Post")]
    public class CreatePolicyTermRequest
    {
        public string PolicyNumber { get; set; }
    }

    public class CreatePolicyTermResponse
    {

    }
}