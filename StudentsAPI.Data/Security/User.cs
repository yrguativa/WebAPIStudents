
using Microsoft.AspNetCore.Identity;

namespace StudentsAPI.Data.Security
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
