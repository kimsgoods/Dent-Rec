using System.Security.Claims;
using DentRec.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentRec.API.Controllers
{
    public class AccountController(SignInManager<AppUser> signInManager) : BaseApiController
    {
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("auth-status")]
        public ActionResult GetAuthState()
        {
            return Ok(new { IsAuthenticated = User.Identity?.IsAuthenticated ?? false });
        }

        [AllowAnonymous]
        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated == false) return NoContent();

            var user = await signInManager.UserManager.GetUserAsync(User);

            return Ok(new { user?.FirstName, user?.LastName, user?.Email, Roles = User.FindFirstValue(ClaimTypes.Role) });
        }
    }
}
