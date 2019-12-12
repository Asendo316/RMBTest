using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Domain.v1.user
{
    public class UserProfile
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string ProfilePicture { get; set; }

        public string DOB { get; set; }

        public int AccountNumber { get; set; }


        public DateTime DateCreated { get; set; }

    }
}
