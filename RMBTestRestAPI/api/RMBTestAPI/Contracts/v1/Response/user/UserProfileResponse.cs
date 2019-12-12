using RMBTestRestAPI.Domain.v1.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Contracts.v1.Response.user
{
    public class UserProfileResponse
    {

        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string ProfilePcture { get; set; }

        public string DOB { get; set; }

        public bool Status { get; set; }

        //public AccountData AccountData { get; set; }
    }
}
