using System;
using Microsoft.AspNetCore.Identity;

namespace P335_BackEnd.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

