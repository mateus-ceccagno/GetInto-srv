using Microsoft.AspNetCore.Identity;

namespace GetInto.Domain.Identity
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
