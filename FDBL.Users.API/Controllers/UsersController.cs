using FDBL.Common.Classes;
using FDBL.Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FDBL.Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<FDBLUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(UserManager<FDBLUser> userManager, RoleManager<IdentityRole> roleManager) => (_userManager, _roleManager) = (userManager, roleManager);

        private async Task<IResult> AddUser(string email, string password)
        {
            try
            {
                if (!ModelState.IsValid) return Results.BadRequest();

                var existingUser = await _userManager.FindByEmailAsync(email);
                if (existingUser is not null) return Results.BadRequest();

                FDBLUser newUser = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, password);

                if (result.Succeeded) return Results.Ok();
            }
            catch { }

            return Results.BadRequest();
        }

        private async Task<IResult> AddRoles(string email, List<string> roles)
        {
            try
            {
                if (!ModelState.IsValid) return Results.BadRequest();

                var user = await _userManager.FindByEmailAsync(email);
                if (user is null) return Results.BadRequest();

                IdentityResult result = await _userManager.AddToRolesAsync(user, roles);

                if (result.Succeeded) return Results.Ok();
            }
            catch { }

            return Results.BadRequest();
        }

        [Route("api/users/seed")]
        [HttpPost]
        public async Task<IResult> Seed()
        {
            try
            {
                await _roleManager.CreateAsync(new IdentityRole { Id = "1", Name = UserRole.Admin });
                await _roleManager.CreateAsync(new IdentityRole { Id = "2", Name = UserRole.Customer });
                await _roleManager.CreateAsync(new IdentityRole { Id = "3", Name = UserRole.Registered });

                var marco = "marco@fdbl.com";
                var nicole = "nicole@fdbl.com";
                var password = "Pass123__";

                await AddUser(marco, password);
                await AddRoles(marco, new List<string> { UserRole.Admin, UserRole.Customer, UserRole.Registered });
                await AddUser(nicole, password);
                await AddRoles(nicole, new List<string> { UserRole.Customer, UserRole.Registered });

                return Results.Ok();
            }
            catch { }

            return Results.BadRequest();
        }


        [Route("api/users/register")]
        [HttpPost]
        public async Task<IResult> Register(RegisterUserDTO registerUserDTO)
        {
            try
            {
                var result = await AddUser(registerUserDTO.Email, registerUserDTO.Password);

                if (result.Equals(Results.BadRequest())) return Results.BadRequest();

                result = await AddRoles(registerUserDTO.Email, registerUserDTO.Roles);

                return Results.Ok();
            }
            catch { }

            return Results.BadRequest();
        }

        [Route("api/users/paid")]
        [HttpPost]
        public async Task<IResult> Paid(PaidCustomerDTO paidCustomerDTO) =>
        await AddRoles(paidCustomerDTO.Email, new List<string> { UserRole.Customer });
    }
}
