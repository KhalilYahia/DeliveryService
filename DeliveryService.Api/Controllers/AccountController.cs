using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Security.Claims;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

using PagedList;
using DeliveryService.WebApi.AssestanceClasses;
using DeliveryService.Domain.Entities;
using AutoMapper;
using DeliveryService.Services.Iservices;
using DeliveryService.Services.DTO;
using DeliveryService.Common;


namespace DeliveryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ApiBaseController
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly ITokenService _tokenGenerator;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _host;
       
        private readonly IUserService _IUserService;


        private readonly IMapper _mapper;
        public AccountController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager,
            ITokenService _tokenGenerator, Microsoft.AspNetCore.Hosting.IHostingEnvironment _host, IUserService iUserService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._tokenGenerator = _tokenGenerator;
            this._host = _host;
           
            _IUserService = iUserService;
            _mapper = mapper;

        }

        // Get user by email
        [Authorize(Roles = "Admin")]
        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound($"User with email {email} not found.");
            }
            var role = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                Token = "",
                Role = role.FirstOrDefault(),
                PhoneNumber = user.PhoneNumber,
                DisplayName=user.DisplayName==null?"Name": user.DisplayName
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("EditUserInfo")]
        public async Task<ActionResult<bool>> EditUserInfo(UserDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);

                if (user == null)
                {
                    return NotFound("User not found.");
                }
                user.PhoneNumber = dto.PhoneNumber;
                user.DisplayName = dto.DisplayName;

                var old_role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                if (old_role != dto.Roll)
                {
                    await _userManager.RemoveFromRoleAsync(user, old_role);
                    await _userManager.AddToRoleAsync(user, dto.Roll);
                }
                await _userManager.UpdateAsync(user);

                return Ok(true);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles="Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(int page)
        {
            int pageSize = 10;
            var user = await _IUserService.GetAllUsers_forAdmin();
           
            if (user == null)
            {
                return NotFound($"not users");
            }
            var res_ = user.ToPagedList(page, pageSize);
           
            return Ok(new PagedResponse<List<UserDto>>(res_.ToList(), page,pageSize,res_.PageCount, res_.TotalItemCount));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsersByRole")]
        public async Task<IActionResult> GetAllUsersByRole(int page,string roleName)
        {
            int pageSize = 100;
            var user = await _userManager.GetUsersInRoleAsync(roleName);

            if (user == null)
            {
                return NotFound($"not users");
            }
            var dto = _mapper.Map<List<UserDto>>(user);
            var res_ = dto.ToPagedList(page, pageSize);

            return Ok(new PagedResponse<List<UserDto>>(res_.ToList(), page, pageSize, res_.PageCount, res_.TotalItemCount));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            int pageSize = 10;
            var roles_ = await _IUserService.GetAllRoles_forAdmin(); 

            if (roles_ == null)
            {
                 return NotFound($"not roles");
            }
         

            return Ok(roles_);
        }

        // Register action
        [HttpPost("RegisterNormalUser")]
        public async Task<ActionResult<CustomUser>> RegisterNormalUser(RegsiterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new CustomUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.NormalUserRole);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return CreatedAtAction(nameof(GetUser), new { email = user.Email }, user);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return ValidationProblem(ModelState);
            }

            return BadRequest(ModelState);
        }


        [HttpPost]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return BadRequest(ModelState);
            }

            //var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            //var userIdentity = new ClaimsIdentity(claims.ToArray(), "Login");
            //var principal = new ClaimsPrincipal(new[] { userIdentity });

            var role = await _userManager.GetRolesAsync(user);
            var token = _tokenGenerator.CreateToken(user, role.FirstOrDefault());

          
            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                Token = token,
                Role = role.FirstOrDefault(),
                
            });
        }


        ////[Authorize]
        ////[HttpPost("logout")]
        ////public async Task<IActionResult> Logout()
        ////{
        ////    // Revoke the token by setting another expiration time in the past
        ////    var expiration = DateTime.UtcNow.AddDays(-15);
        ////    var jwt_SH = new JwtSecurityTokenHandler();
        ////    var token = jwt_SH.WriteToken(new JwtSecurityToken(
        ////        expires: expiration
        ////    ));

        ////    return Ok(new { message = "Logged out successfully" });
        ////}

     

        ////[HttpGet("Addnewpassword")]
        ////public ActionResult Addnewpassword(string userId, string code)
        ////{
        ////    return Ok();
        ////}

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (ModelState.IsValid)
            {               
                // Find the user by email
                var user = await _userManager.FindByIdAsync(GetuserId);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                if (user.Email != model.Email)
                {
                    return NotFound("User not found");
                }
                // Change the password
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    return Ok("Password changed successfully");
                }

                // If there were errors, add them to ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return ValidationProblem(ModelState);
            }

            return BadRequest(ModelState);
        }
    }
   
}
