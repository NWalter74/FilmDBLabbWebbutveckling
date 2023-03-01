using System.Security.Claims;
using System.Xml.Linq;

namespace FDBL.Common.JWT;

public static class JwtParser
{
    private static byte[] ParseBase64Payload(string payload)
    {
        switch (payload.Length % 4)
        {
            case 2:
                payload += "==";
                break;
            case 3:
                payload += "=";
                break;
        }

        return Convert.FromBase64String(payload);
    }

    private static void ExtractClaimsFormPayload(List<Claim> claims, Dictionary<string, object> jwtProperties)
    {
        jwtProperties.TryGetValue(ClaimTypes.Role, out var roles);
        if (roles is null) return;

        var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');

        if (parsedRoles.Length == 0) claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));

        foreach (var parsedRole in parsedRoles)
            claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));

        jwtProperties.Remove(ClaimTypes.Role);

        claims.AddRange(jwtProperties.Select(jp => new Claim(jp.Key, jp.Value.ToString() ?? string.Empty)));
    }

    public static List<Claim> ParseClaimsFromPayload(string jwt)
    {
        var claims = new List<Claim>();
        if (string.IsNullOrWhiteSpace(jwt)) return claims;
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64Payload(payload);
        var jwtProperties = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        ExtractClaimsFormPayload(claims, jwtProperties);
        return claims;
    }

    public static SignUpUserDTO? ParseUserInfoFromPayload(string jwt)
    {
        try
        {
            var claims = ParseClaimsFromPayload(jwt);
            var email = claims.SingleOrDefault(c => c.Type.Equals("email"))?.Value.ToString() ?? string.Empty;

            return new SignUpUserDTO(email, claims);
        }
        catch (Exception ex)
        {
        }

        return null;
    }

    public static bool ParseIsInRoleFromPayload(string jwt, string role)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(jwt)) return false;

            List<Claim>? claims = ParseUserInfoFromPayload(jwt)?.Roles;

            if (claims is null || claims.Count.Equals(0)) return false;

            var isInRole = claims.Exists(c => c.Value.Equals(role));

            return isInRole;
        }
        catch (Exception ex)
        {
        }

        return false;
    }

    public static bool ParseIsNotInRoleFromPayload(string jwt, string role) => !ParseIsInRoleFromPayload(jwt, role);

    //a public static bool method named CompareTokenClaims with two string? parameters named token1 and token2.
    public static bool CompareTokenClaims(string? token1, string? token2)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(token1) || string.IsNullOrWhiteSpace(token2)) return false;

            //Call the ParseClaimsFromPayload method to fetch the Role claims for both tokens and store them in variables named cliams1 and claims2
            var claims1 = ParseClaimsFromPayload(token1).Where(c => c.Type.Equals(ClaimTypes.Role));
            var claims2 = ParseClaimsFromPayload(token2).Where(c => c.Type.Equals(ClaimTypes.Role));

            //Return false if the the number of clains in claims1 and claims2 differ.
            if (claims1.Count() != claims2.Count()) return false;

            var success = true;

            //Iterate over the claims in claims1 and compare each cliam with the claims in claims2 and return false if their claims differ.
            foreach (var claim in claims1)
            {
                if (!claims2.Any(c => c.Value.Equals(claim.Value))) { success = false; break; }
            }

            return success;

        }
        //Return false if either of the tokens are null or an empty string.
        catch { return false; }
    }
}
