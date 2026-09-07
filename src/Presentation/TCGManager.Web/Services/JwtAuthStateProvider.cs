using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace TCGManager.Web.Services;

public class JwtAuthStateProvider : AuthenticationStateProvider
{
    private static readonly AuthenticationState Anonymous =
        new(new ClaimsPrincipal(new ClaimsIdentity()));

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        Task.FromResult(Anonymous);
}