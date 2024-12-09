using AutoMapper;
using Learnify.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Utility.SD;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

       [HttpPost("Regester")]
    public async Task<IActionResult> Regester(ApplicationUserDTO userDTO)
    {
        if (roleManager.Roles.IsNullOrEmpty())
        {
            await roleManager.CreateAsync(new(SD.AdminRoll));
            await roleManager.CreateAsync(new(SD.InstructorRoll));
            await roleManager.CreateAsync(new(SD.StudentRoll));
        }

        if (ModelState.IsValid)
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = userDTO.Name,
                Email = userDTO.Email,
                City = userDTO.City,
            };

            var resualt = await userManager.CreateAsync(applicationUser, userDTO.Password);
            if (resualt.Succeeded)
            {
                await userManager.AddToRoleAsync(applicationUser, "Student");
                await signInManager.SignInAsync(applicationUser,false);
            }
            ModelState.AddModelError("Password", "InvalidPassword");
        }
        return BadRequest(userDTO);
    }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginDTO.Name);
                if (user != null)
                {
                    var result = await userManager.CheckPasswordAsync(user, loginDTO.Password);

                    if (result)
                    {
                        await signInManager.SignInAsync(user, false);
                        return Ok(user);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "There are errors");
                    }
                }
                return NotFound();
            }
            return BadRequest(loginDTO);
        }

        [HttpDelete("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(forgotPasswordDTO.Email);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                // You would send this token via email in a real application
                return Ok(new { ResetToken = token });
            }
            return BadRequest(ModelState);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(resetPasswordDTO.Email);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                var result = await userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.NewPassword);
                if (result.Succeeded)
                {
                    return Ok("Password has been reset.");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var userProfile = new UserProfileDTO
            {
                Name = user.UserName,
                Email = user.Email,
                City = user.City,
                ProfilePicture = null
            };

            return Ok(userProfile);
        }
        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UserProfileDTO userProfileDTO)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            if (ModelState.IsValid)
            {
                // Update user fields
                user.UserName = userProfileDTO.Name ?? user.UserName;
                user.Email = userProfileDTO.Email ?? user.Email;
                user.City = userProfileDTO.City ?? user.City;

                // Handle profile picture upload
                if (userProfileDTO.ProfilePicture != null)
                {
                    var uploadsFolder = Path.Combine("wwwroot", "images", "profile-pictures");
                    Directory.CreateDirectory(uploadsFolder);
                    var fileName = $"{Guid.NewGuid()}_{userProfileDTO.ProfilePicture.FileName}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await userProfileDTO.ProfilePicture.CopyToAsync(stream);
                    }

                    user.ProfilePictureUrl = $"/images/profile-pictures/{fileName}";
                }

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok("Profile updated successfully.");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return BadRequest(ModelState);
        }


    }
}
