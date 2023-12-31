using System.Security.Claims;

namespace Mytask.API.Services;

public class IdentityService : IIdentityService
{
    private IHttpContextAccessor _context;

    public IdentityService(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public string GetUserIdentity()
    {
        return _context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}