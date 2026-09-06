using System.Net.Http.Json;

namespace TCGManager.Web.Services;

public class AuthService(HttpClient httpClient) : IAuthService
{
    private bool _isAuthenticated = false;

    public async Task<bool> LoginAsync(string email, string password)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(
                "api/v1/auth/login",
                new { email, password });

            _isAuthenticated = response.IsSuccessStatusCode;
            return _isAuthenticated;
        }
        catch
        {
            return false;
        }
    }

    public Task LogoutAsync()
    {
        _isAuthenticated = false;
        return Task.CompletedTask;
    }

    public Task<bool> IsAuthenticatedAsync() =>
        Task.FromResult(_isAuthenticated);
}