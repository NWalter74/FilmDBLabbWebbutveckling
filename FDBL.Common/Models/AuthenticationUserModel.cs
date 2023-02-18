namespace FDBL.Common.Models;

public class AuthenticationUserModel
{
    [Required(ErrorMessage = "Email Required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password Required")]
    public string Password { get; set; } = string.Empty;
}
