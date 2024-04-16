using System.Security.Claims;

namespace ShoesShopProject.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirstValue(ClaimTypes.Email);
        }
    }
}
