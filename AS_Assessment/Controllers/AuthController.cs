using AS_Assessment.Data;
using AS_Assessment.DTOs;
using AS_Assessment.Models;
using AS_Assessment.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AS_Assessment.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly ApplicationDbContext _context;

        public AuthController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              TokenService tokenService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }


        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(dto);
            }

            return RedirectToAction("Login", "Auth");
        }



        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Invalid credentials");

            var accessToken = _tokenService.CreateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            refreshToken.UserId = user.Id;
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                token = accessToken,
                refreshToken = refreshToken.Token
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var storedToken = await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (storedToken == null || !storedToken.IsActive)
                return Unauthorized("Invalid or expired refresh token");

            var newAccessToken = _tokenService.CreateAccessToken(storedToken.User);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Invalidate the old one
            storedToken.Revoked = DateTime.UtcNow;

            // Add new one
            newRefreshToken.UserId = storedToken.UserId;
            _context.RefreshTokens.Add(newRefreshToken);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                token = newAccessToken,
                refreshToken = newRefreshToken.Token
            });
        }

        [HttpGet("logout")]
        public IActionResult LogoutView()
        {
            return View("Logout");
        }


        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userTokens = _context.RefreshTokens
                                     .Where(rt => rt.UserId == userId)
                                     .AsEnumerable()
                                     .Where(rt => rt.IsActive)
                                     .ToList();

            foreach (var token in userTokens)
            {
                token.Revoked = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Login", "Auth");
        }


        [HttpPost("web-login")]
        public async Task<IActionResult> WebLogin([FromForm] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                ModelState.AddModelError("", "Invalid login");
                return View("Login");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }


    }
}
