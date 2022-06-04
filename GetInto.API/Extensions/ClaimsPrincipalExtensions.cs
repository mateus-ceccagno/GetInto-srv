using System.Security.Claims;

namespace GetInto.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static long GetUserId(this ClaimsPrincipal user)
        {
            return long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
