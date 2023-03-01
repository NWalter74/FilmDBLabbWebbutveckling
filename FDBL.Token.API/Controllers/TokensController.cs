using Azure.Core;

namespace FDBL.Token.API.Controllers;

[ApiController]
public class TokensController : ControllerBase
{
    private readonly ITokenService _tokenService;

    //Add a constructor and inject the ITokenService and store the instance in a private readonly variable named _tokenService.
    public TokensController(ITokenService tokenService) => _tokenService = tokenService;


    //Add public HttpPost method named Get with a LoginUserDTO parameter named loginUser sent in through the request body.
    //Decorate the method with the api/tokens route
    [Route("api/tokens")]
    [HttpPost]
    public async Task<IResult> Get([FromBody] LoginUserDTO loginUser)
    {
        try
        {
            //call the GetTokenAsync method on the injected _tokenService and store the result in a varaible named result
            var result = await _tokenService.GetTokenAsync(loginUser);

            //If either the AccessToken or UserName properties of the result object are null or an empty string, return Unauthorized.
            if (string.IsNullOrWhiteSpace(result.AccessToken) ||
                string.IsNullOrWhiteSpace(result.UserName)) return Results.Unauthorized();

            //Return Ok with the result object.
            return Results.Ok(result);
        }
        catch
        {
        }
        //Return Unauthorized blow the catch’s ending curly brace.
        return Results.Unauthorized();
    }

    //Add public HttpPost method named Create with a TokenUserDTO parameter named tokenUser sent in through the request body.
    //Decorate the method with the api/tokens/create route.
    [Route("api/tokens/create")]
    [HttpPost]
    public async Task<IResult> Create(TokenUserDTO tokenUserDTO)
    {
        try
        {
            //call the GenerateTokenAsync method on the injected _tokenService and store the result in a varaible named jwt.
            var jwt = await _tokenService.GenerateTokenAsync(tokenUserDTO);

            //Return Unauthorized. If the jwt varaible is null or an empty string.
            if (string.IsNullOrWhiteSpace(jwt)) return Results.Unauthorized();

            //Return Created with the string Token as the URI and the jwt object
            return Results.Created("Token", jwt);
        }
        catch
        {
        }
        //Return Unauthorized blow the catch’s ending curly brace
        return Results.Unauthorized();
    }

}
