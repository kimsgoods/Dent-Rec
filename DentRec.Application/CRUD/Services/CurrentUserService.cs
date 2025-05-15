using DentRec.Application.CRUD.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DentRec.Application.CRUD.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public string GetUserName()
        {
            if (httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated != true)
            {
                return string.Empty;
            }

            return httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        }
    }
}
