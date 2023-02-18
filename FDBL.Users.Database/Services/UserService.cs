namespace FDBL.Users.Database.Services;

public class UserService : IUserService
{
    private readonly UserManager<FDBLUser> _userManager;

    public UserService(UserManager<FDBLUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<FDBLUser?> GetUserAsync(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }
        catch
        {
        }

        return default;
    }

    public async Task<FDBLUser?> GetUserAsync(LoginUserDTO loginUser)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);

            if (user is null) return default;

            var hasher = new PasswordHasher<FDBLUser>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash!, loginUser.Password);

            if (result.Equals(PasswordVerificationResult.Success)) return user;
        }
        catch
        {
        }

        return default;
    }
}
