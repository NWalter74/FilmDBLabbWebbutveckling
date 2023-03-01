
namespace FDBL.Token.API.Services;

public class TokenService : ITokenService
{
    #region Properties
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly UserManager<FDBLUser> _userManager;
    #endregion

    #region Constructors
    //Inject the IConfiguration service named _configuration to gain access to the properties in the appsettings.json file
    //Inject the IUserService service named _userService to gain access to the database.
    //Inject the UserManager<VODUser> service named _userManager to gain access user accounts
    public TokenService(IConfiguration configuration, IUserService userService, UserManager<FDBLUser> userManager)
    {
        _configuration = configuration;
        _userService = userService;
        _userManager = userManager;
    }
    #endregion

    #region Helper Methods
    /*In the TokenService class, add a nullable string? method with an List<string> parameter 
     *named roles, which receives a list of role names to add as claims to the token, an VODUser
     *parameter named user*/
    private string? CreateToken(IList<string>? roles, FDBLUser user)
    {
        try
        {
            /*use the _configuration service to check that the Jwt settings in the appsettings.json file, and the roles and user parameters are not null, and throw an 
             *ArgumentException with the message JWT configuration missing if any of them are*/
            if (_configuration["Jwt:SigningSecret"] is null ||
               _configuration["Jwt:Duration"] is null ||
               _configuration["Jwt:Issuer"] is null ||
               _configuration["Jwt:Audience"] is null ||
               roles is null || user is null)
                throw new ArgumentException("JWT configuration missing.");

            /*Use the Convert class’ FromBase64String method to convert the Jwt:SigningSecret string in the application.josn file into an 8-bit unsigned int byte array, which is the format required 
             *for the token’s signing credentials. Store the result in a variable named singningKey.*/
            var signingKey = Convert.FromBase64String(_configuration["Jwt:SigningSecret"] ?? "");

            /*Create the token credentials by careating an instance of the SigningCredentials class and pass its constructor the signingKey formatted as a security key with the 
             *SymmetricSecurityKey class using the HMAC256 algorithm. Store the credentials in a variable with named credentials.*/
            var credentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature);

            //Fetch the Jwt:Duration proeprty from the application.josn file and parse the string into an int variable named duration.
            var duration = int.Parse(_configuration["Jwt:Duration"] ?? "");

            //Use the DateTimeOffset class to convert the current date and time to the Unix time format used by JWT tokens. Store the date in a varaible name now.
            var now = EpochTime.GetIntDate(DateTime.UtcNow).ToString();// new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();

            /*Calculate the token’s expiration date and store the date in a varaible name expires. Use the DateTimeOffset class and the AddDays method to convert the current date and add the 
             *number of days specified in the duration varaible to the Unix time format used by JWT tokens.*/
            var expires = EpochTime.GetIntDate(DateTime.UtcNow.AddDays(duration)).ToString(); // new DateTimeOffset(DateTime.UtcNow.AddDays(duration)).ToUnixTimeSeconds().ToString();

            //Create a new List<Claim> collaction named claims and add the base claims to it
            //Claim Types: https://datatracker.ietf.org/doc/html/rfc7519#section-4
            List<Claim> claims = new() {
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"] ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"] ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Nbf, now),
                new Claim(JwtRegisteredClaimNames.Exp, expires),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id)
            };

            //Iterate over the roles names in the roles collection parameter and add them as Role claims to the claims collection using the Claim class.
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            /*Format the token by crating an instance of the JwtSecurityToken class and pass its constructor an instance of the JwtHeader class, which you pass the credentials varaible 
             *containing the signing key, and an instance of the JwtPayload class, which you pass the claims collaection. Store the result in a variable named jwtToken.*/
            var jwtToken = new JwtSecurityToken(
                new JwtHeader(credentials),
                new JwtPayload(claims)
            );

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            //Create the token by calling the WriteToken method on an instance of the JwtSecurityTokenHandler class and store the result in a variable named token.
            var token = jwtTokenHandler.WriteToken(jwtToken);

            //Return the token from the method
            return token;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region Token Methods
    public async Task<string?> GenerateTokenAsync(TokenUserDTO tokenUserDTO)
    {
        try
        {
            //use the _userService service fetch the user matching the email in the tokenUserDTO instance and store it in a varaible named user.
            var user = await _userService.GetUserAsync(tokenUserDTO.Email);

            //Throw an UnauthorizedAccessException if the user varaible is null, i.e. the user does not exist
            if (user is null) throw new UnauthorizedAccessException();

            //Fetch the user’s roles with the GetRolesAsync method in the UserManager service and store them in a variable named roles.
            var roles = await _userManager.GetRolesAsync(user);

            //Call the CreateToken method you created earlier with the roles and the user. And store the returned JWT in a varaible named token.
            var token = CreateToken(roles, user);

            //Add an if block checking if the tokenDTO.Save property is true
            if (tokenUserDTO.Save)
            {
                /*store the token in the AspNetUserTokens table by calling the SetAuthenticationTokenAsync method in the UserManager service and store the result in a
                 *variable named result.*/
                var result = await _userManager.SetAuthenticationTokenAsync(user, "FDBL", "UserToken", token);

                //Throw a SecurityTokenException with the message Could not add token to user if result isn’t equal to IdentityResult.Success.
                if (result != IdentityResult.Success)
                    throw new SecurityTokenException("Could not add token to user");
            }
            //return the JWT in the token variable
            return token;
        }
        catch
        {
            throw;
        }
    }

    public async Task<AuthenticatedUserDTO> GetTokenAsync(LoginUserDTO loginUserDTO)
    {
        try
        {
            // throw an UnauthorizedAccessException if the loginUserDTO parameter is null
            if (loginUserDTO is null) throw new UnauthorizedAccessException();

            //Use the _userService service fetch the user matching the email and password in the loginUserDTO instance and store it in a varaible named user.
            var user = await _userService.GetUserAsync(loginUserDTO);

            //Throw an UnauthorizedAccessException if the user varaible is null, i.e. the user does not exist.
            if (user is null) throw new UnauthorizedAccessException();

            /*Fetch the token in the AspNetUserTokens table by calling the GetAuthenticationTokenAsync method on the UserManager service and store the result in 
             *a variable named token.*/
            var token = await _userManager.GetAuthenticationTokenAsync(user, "FDBL", "UserToken");

            /*Call the GenerateTokenAsync method and pass it an instance of the TokenDTO record with the email and false to skip saving the token to the database. Store the result in a variable 
             *named compareToken*/
            var compareToken = await GenerateTokenAsync(new TokenUserDTO(loginUserDTO.Email, false));

            //Call the JwtParser.CompareTokenClaims with the token and compareToken values and store the result in a variable named success.
            var success = JwtParser.CompareTokenClaims(token, compareToken);

            /*If success it false, call the GenerateTokenAsync method and pass it an instance of the TokenDTO record with only the email to use the deafult true value for the other parameter 
             *to save the token in the database. Store the returned token in the existing token varaible*/
            if (success == false) token = await GenerateTokenAsync(new TokenUserDTO(loginUserDTO.Email));

            //Return an instance of the AuthenticatedUserDTO with token and user name.
            return new AuthenticatedUserDTO(token, user.UserName);
        }
        catch
        {
            throw;
        }
    }

    #endregion
}