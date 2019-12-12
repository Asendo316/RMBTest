using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Domain.v1.user
{
    public class AccountData
    {
        [Key]
        public Guid guid { get; set; }

        public int AccountNumber { get; set; }

        public double LedgerBalance { get; set; }

        public double CurrentBalance { get; set; }

    }
}
