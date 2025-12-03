using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public static class JwtPrincipalFactory
{
    public static ClaimsPrincipal? FromBearerHeaderWithoutValidation(Dictionary<string, string> headers)
    {
        if(headers.TryGetValue("Authorization", out string authHeader) == false)
            return null;

        if (string.IsNullOrWhiteSpace(authHeader))
            return null;

        if (!authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return null;

        var rawToken = authHeader["Bearer ".Length..].Trim();

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(rawToken);

        var identity = new ClaimsIdentity(jwt.Claims, "Auth0", ClaimTypes.Name, ClaimTypes.Role);
        return new ClaimsPrincipal(identity);
    }
}
