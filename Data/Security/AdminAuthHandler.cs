using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace FutbolowaJaskinia.Data.Security
{
    public class AdminAuthHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            var user = context.User;

            if (user.Identity.IsAuthenticated && user.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
