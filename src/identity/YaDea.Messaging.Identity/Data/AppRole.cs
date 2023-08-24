using Microsoft.AspNetCore.Identity;

namespace YaDea.Messaging.Identity.Data
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}