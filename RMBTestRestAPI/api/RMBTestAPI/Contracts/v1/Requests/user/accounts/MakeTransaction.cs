using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Contracts.v1.Requests.user.accounts
{
    public class MakeTransaction
    {
        object AccountNumber { get; set; }
        double Amount { get; set; }

    }
}
