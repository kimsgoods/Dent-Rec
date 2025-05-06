using DentRec.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Application.Services
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
