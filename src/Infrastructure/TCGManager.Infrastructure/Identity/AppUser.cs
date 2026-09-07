using Microsoft.AspNetCore.Identity;

namespace TCGManager.Infrastructure.Identity;

public class AppUser : IdentityUser
{
    public string? DisplayName { get; set; }
    public string PreferredLanguage { get; set; } = "fr";
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}