using System.Security.Claims;

namespace mytask.api.Services;

public class IdentityService: IIdentityService
{
    private ClaimsPrincipal _principal;

    public IdentityService(ClaimsPrincipal principal)
    {
        _principal = principal ?? throw new ArgumentNullException(nameof(principal));
    }

    public string GetUserIdentity()
    {
        return _principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}