using Microsoft.AspNetCore.Identity;

namespace YaDea.Messaging.Identity.Data
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Description { get; set; }
    }
}