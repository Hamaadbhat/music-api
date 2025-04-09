using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myMusicApi.model;

namespace myMusicApi.Controller
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        public IActionResult register(userDTO user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            ApplicationUser myUser = new ApplicationUser()
            {
                UserName = user.userName,
                FirstName = user.firstName,
                LastName = user.lastName,
                PasswordHash = user.passwordHash,
                Email = user.email,
                PhoneNumber=user.phoneNumber
            };




            IdentityResult result = _userManager.CreateAsync(myUser, myUser.PasswordHash).Result;
            if (result.Succeeded)
            {
                _signInManager.SignInAsync(myUser, isPersistent: false);
                return Ok(user);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

        }
    }
}
