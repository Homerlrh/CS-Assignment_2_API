using Assignment2.Data;
using Assignment2.Repositories;
using Assignment2.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _config;
        private IServiceProvider _serviceProvider;
        private ApplicationDbContext _context;


        public RegisterController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
                                IConfiguration config,
                                IServiceProvider serviceProvider,
                                ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _serviceProvider = serviceProvider;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync([FromBody] RegisVM RegisVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = RegisVM.Username, Email = RegisVM.Email, };
                var result = await _userManager.CreateAsync(user, RegisVM.Password);
                if (result.Succeeded)
                {
                    ClientRepo cRP = new ClientRepo(_context);
                    bool isNewClient = cRP.Create(RegisVM.LastName, RegisVM.FirstName, RegisVM.Email);

                    if (isNewClient)
                    {

                        var tokenString = GenerateJSONWebToken(user);
                        var jsonOK = new
                        {
                            tokenString = tokenString,
                            StatusCode = "OK",
                            currentUser = RegisVM.Email
                        };

                        return new ObjectResult(jsonOK);

                    }
                }
            }
            var jsonInvalid = new { tokenString = "", StatusCode = "Invalid Login." };
            return new ObjectResult(jsonInvalid);
        }


        List<Claim> AddUserRoleClaims(List<Claim> claims, string userId)
        {
            // Get current user's roles. 
            var userRoleList = _context.UserRoles.Where(ur => ur.UserId == userId);
            var roleList = from ur in userRoleList
                           from r in _context.Roles
                           where r.Id == ur.RoleId
                           select new { r.Name };

            // Add each of the user's roles to the claims list.
            foreach (var roleItem in roleList)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleItem.Name));
            }
            return claims;
        }

        string GenerateJSONWebToken(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> {

            new Claim(JwtRegisteredClaimNames.Sub, user.Email),

            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            new Claim(ClaimTypes.NameIdentifier, user.Id)

            };

            claims = AddUserRoleClaims(claims, user.Id);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
