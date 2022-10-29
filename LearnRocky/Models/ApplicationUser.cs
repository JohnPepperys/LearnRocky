using Microsoft.AspNetCore.Identity;

namespace LearnRocky.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string userFullName { get; set; }    
    }
}
