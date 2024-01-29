using System.Security.Claims;

namespace Net6Api.Domain.Helper
{
    public static class UseStaticHttpContext
    {
        private static readonly IHttpContextAccessor _httpContextAccessor;
        static UseStaticHttpContext() {
            _httpContextAccessor=new HttpContextAccessor();
        }
        public static string? UserName => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
    }
}
