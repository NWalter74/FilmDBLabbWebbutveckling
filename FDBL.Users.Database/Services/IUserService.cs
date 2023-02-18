namespace FDBL.Users.Database.Services;

public interface IUserService
{
    Task<FDBLUser?> GetUserAsync(LoginUserDTO loginUser);
    Task<FDBLUser?> GetUserAsync(string email);
}