using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace FDBL.Common.JWT;

public static class JwtParser
{
    /*Add private static byte[] method named ParseBase64Payload, which adds padding to the end of the base 64 string passed-in through its string payload parameter and parses it into a 
     *byte array needed to desrialize the JSON payload data (the middle part of the JWT.)*/
    private static byte[] ParseBase64Payload(string payload)
    {
        //Insde the method, add one equal sign to the end of the string if the remainder of payload.Length % 4 is 3, and two equal signs if it is 2
        switch (payload.Length % 4)
        {
            case 2:
                payload += "==";
                break;
            case 3:
                payload += "=";
                break;
        }

        //Convert the string into a byre array and return it.
        return Convert.FromBase64String(payload);
    }

    /*Add private static method named ExtractClaimsFromPayload with two parameters, one List<Cliam> named claims and one Dictionary<string, object> named JwtProperties, to the
     *JwtParser class.*/
    private static void ExtractClaimsFormPayload(List<Claim> claims, Dictionary<string, object> jwtProperties)
    {
        //Extreac the roles using the ClaimTypes.Role value and store the result in a varaible named roles
        jwtProperties.TryGetValue(ClaimTypes.Role, out var roles);

        //Return from the method if the roles varaible is null
        if (roles is null) return;

        /*The roles in the roles variable are stored as a comma-separated JSON array ([p1:v1, p2:v2]), which you must split into a string array. Trim any spaces from the start and end of the string, 
         *then trim the start and end square brackets, then split the string on any comma. Store the result in a variable named paresdRoles.*/
        var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');

        if (parsedRoles.Length == 0) claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));

        //Iterate over the roles in the paresdRoles variable and trim off any quotation marks from the strings and add them as Role claims to the claims collaction.
        foreach (var parsedRole in parsedRoles)
            claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));

        //To parse the rest of the claims, you need to remove the Role claim property from the jwtProperties claims list.
        jwtProperties.Remove(ClaimTypes.Role);

        //Add the rest of the claims to the claims collection.
        claims.AddRange(jwtProperties.Select(jp => new Claim(jp.Key, jp.Value.ToString() ?? string.Empty)));
    }

    //Add public static method named ParseClaimsFromPayload with one string parameter named jwt that returns a List<Claim> to the JwtParser class.
    public static List<Claim> ParseClaimsFromPayload(string jwt)
    {
        //Add a List<Claim> collection named cliams, which will hold any claim found in the JWT payload.
        var claims = new List<Claim>();

        //Return the empty claims collection if the jwt is null or an empty string
        if (string.IsNullOrWhiteSpace(jwt)) return claims;

        //Split the jwt on any period (.) and assign the payload (middle string) to a variable named payload
        var payload = jwt.Split('.')[1];

        //Call the ParseBase64Payload method with the string in the payload varaible and store the result in a varaible named jsonBytes.
        var jsonBytes = ParseBase64Payload(payload);

        //Use the JsonSerializer.Deserialze method to convert the data in the jsonBytes varaible into JSON key-value pairs properties and store them in a variable named jwtProperties
        var jwtProperties = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        //Call the ExtractClaimsFromPayload method with the claims collection and the JSON properties in the jwtProperties variable.
        ExtractClaimsFormPayload(claims, jwtProperties);

        //Return the claims collection
        return claims;
    }

    //Add public static SignUpUserDTO? method named ParseUserInfoFromPayload with one string parameter named jwt to the JwtParser class.
    public static SignUpUserDTO? ParseUserInfoFromPayload(string jwt)
    {
        try
        {
            //Call the ParseClaimsFromPayload method with the jwt string and store the result in a variable named claims
            var claims = ParseClaimsFromPayload(jwt);

            //Fetch the email from the claims collaction with the SingleOrDefault LINQ method and store it in a variable named email.
            var email = claims.SingleOrDefault(c => c.Type.Equals("email"))?.Value.ToString() ?? string.Empty;

            //Return an instance of the SignUpUserDTO instance with the email and claims
            return new SignUpUserDTO(email, claims);
        }
        catch (Exception ex)
        {
        }

        return null;
    }

    //Add public static bool method named ParseIsInRoleFromPayload with two string parameters named jwt and role to the JwtParser class.
    public static bool ParseIsInRoleFromPayload(string jwt, string role)
    {
        try
        {
            //Return false if the jwt parameter is null or an empty string.
            if (string.IsNullOrWhiteSpace(jwt)) return false;

            //Fetch the user’s claims by calling the ParseUserInfoFromPayload method with the jwt parameter and store the claims in a variable named claims
            List<Claim>? claims = ParseUserInfoFromPayload(jwt)?.Roles;

            //Return false if the claims variable is null or is empty.
            if (claims is null || claims.Count.Equals(0)) return false;

            //Sheck if the claims collection contains a role with the name in the role parameter and store the result in a bool variable named isInRole
            var isInRole = claims.Exists(c => c.Value.Equals(role));

            return isInRole;
        }
        catch (Exception ex)
        {
        }

        return false;
    }

    //Add a method named ParseIsNotInRoleFromPayload, which is the inverse of the ParseUserInfoFromPayload method.
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
