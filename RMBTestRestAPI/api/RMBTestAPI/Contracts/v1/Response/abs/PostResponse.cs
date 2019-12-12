using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Contracts.v1.Response.abs
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
