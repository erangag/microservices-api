using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.Entities
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
