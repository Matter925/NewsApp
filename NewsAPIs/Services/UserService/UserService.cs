
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using NewsAPIs.Models;
using NewsAPIs.Data;
using NewsAPIs.Helpers;
using NewsAPIs.Dtos;

namespace NewsAPIs.Services
{
    
    public class UserService : IUserService
    {
        private IHostingEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, IConfiguration configuration , ApplicationDbContext context ,  IHostingEnvironment environment)
        {
            _environment = environment;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _configuration = configuration;
            _context = context;
        }

        public async Task<AuthModel> RegisterAsync(RegisterDto register)
        {
            if (await _userManager.FindByEmailAsync(register.Email) != null)
                return new AuthModel { Message = " Email is already registered !!" };
            if (await _userManager.FindByNameAsync(register.Username) != null)
                return new AuthModel { Message = " Username is already registered !!" };
            var user = new ApplicationUser
            {
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.Username,

            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description} , ";
                }
                return new AuthModel { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, "User");


            var jwtSecurityToken = await CreateJwtToken(user);
            await _userManager.UpdateAsync(user);
            //----------------------------------------------------------------------------------------------------------------
            
                return new AuthModel
                {
                    Message = "User Registered successfully ",
                    Success = true,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName,
                    
                    Roles = new List<string> { "User" },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    

                };
            }

        

        public async Task<AuthModel> LoginAsync(LoginDto login)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                authModel.Message = "Email or Password is incorrect !".ToString();
                authModel.Success = false;
                return authModel;
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.Message = "User Login successfully ";
            authModel.Success = true;
            authModel.FirstName = user.FirstName;
            authModel.LastName = user.LastName;
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            authModel.Roles = rolesList.ToList();


            return authModel;

        }



        //-----------------------------------Admin Methods--------------------------------

        public async Task<GeneralRetDto> CreateRole(CreateRoleDto createRole)
        {
            if (await _roleManager.RoleExistsAsync(createRole.RoleName))
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = "The Role already exist !!"
                };
            }
            var result = await _roleManager.CreateAsync(new IdentityRole
            {

                Name = createRole.RoleName
            });
            if (result.Succeeded)
            {
                return new GeneralRetDto
                {
                    Success = true,
                    Message = "Successfully"
                };
            }
            return new GeneralRetDto
            {
                Success = false,
                Message = "Sonething went wrong"
            };

        }
        public async Task<GeneralRetDto> AssignRole(AssignRoleDto assignRole)
        {
            var userDatails = await _userManager.FindByEmailAsync(assignRole.Email);
            if (userDatails is null || !await _roleManager.RoleExistsAsync(assignRole.RoleName))
            {
                return new GeneralRetDto
                {
                    Success = false,
                    Message = "Invalid user Email or Role"
                };
            }

            if (await _userManager.IsInRoleAsync(userDatails, assignRole.RoleName))
                return new GeneralRetDto
                {
                    Success = false,
                    Message = "User already assigned to this role",
                };
            var result = await _userManager.AddToRoleAsync(userDatails, assignRole.RoleName);
            if (result.Succeeded)
            {
                return new GeneralRetDto
                {
                    Success = true,
                    Message = "Successfully Assigned",
                };
            }
            return new GeneralRetDto
            {
                Success = false,
                Message = "Sonething went wrong",
            };

        }
        public async Task<List<IdentityRole>> GetRoles()
        {
            var result = await _roleManager.Roles.ToListAsync();
            return result;
        }


        //---------Auth-----------------------------------------------

       

        



        //---------------MethodOfToken--------------
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(10),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        
    }
}