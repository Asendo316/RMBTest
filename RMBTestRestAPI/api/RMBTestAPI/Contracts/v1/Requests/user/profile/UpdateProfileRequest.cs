using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Contracts.v1.Requests.user.profile
{
    public class UpdateProfileRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ProfilePcture { get; set; }
        public string DOB { get; set; }
    }
}
