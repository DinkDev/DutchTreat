namespace DutchTreat.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Data.Entities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using ViewModels;

    public class AccountController : Controller
    {
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            SignInManager<StoreUser> signInManager,
            UserManager<StoreUser> userManager,
            IConfiguration config,
            ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                        model.Username,
                        model.Password,
                        model.RememberMe,
                        false);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        return RedirectToAction("Shop", "App");
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Failed to login");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        // Create the token
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email), // sub = Subject
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            _config["Tokens:Issuer"],
                            _config["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(20),
                            signingCredentials: credentials);

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created("", results);
                    }
                }
            }

            return BadRequest();
        }
    }
}
