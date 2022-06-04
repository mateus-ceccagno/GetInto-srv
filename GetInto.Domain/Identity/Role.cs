using Microsoft.AspNetCore.Identity;

namespace GetInto.Domain.Identity
{
    public class Role : IdentityRole<long>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
