
namespace CommandAPI.Controllers
{
    using System.Text;
    using System.Threading.Tasks;
    using CommandAPI.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/user")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto register)
        {
              var user = new IdentityUser{ UserName = register.Email,Email = register.Email };
              IdentityResult result  = await _userManager.CreateAsync(user,register.Password);
              if(result.Succeeded)
              {
                  await _signInManager.SignInAsync(user,true);
                //   _signInManager.PasswordSignInAsync()
                  return Ok();
              }
              else
              {
                  var errorDetail = new StringBuilder();
                  foreach(var error in result.Errors)
                  {
                        errorDetail.Append(" " + error.Description);
                  }
                  return Problem(errorDetail.ToString());
              }
              
        }
    }

}