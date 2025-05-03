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
            var user = httpContextAccessor.HttpContext.User;
            return user.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        }
    }
}
