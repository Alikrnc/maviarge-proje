using System.Security.Claims;

namespace FSH.WebApi.Application.Common.Interfaces;

public interface ICurrentUser
{
    string? Name { get; }

    Guid GetUserId();

    string? GetUserFirstName();
    string? GetUserLastName();
    string? GetUserEmail();

    string? GetTenant();

    bool IsAuthenticated();

    bool IsInRole(string role);

    IEnumerable<Claim>? GetUserClaims();
}