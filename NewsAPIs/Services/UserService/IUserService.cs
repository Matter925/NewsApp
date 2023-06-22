using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using NewsAPIs.Dtos;
using NewsAPIs.Models;

namespace NewsAPIs.Services
{
    public interface IUserService
    {
        Task<AuthModel> RegisterAsync(RegisterDto register);
        Task<AuthModel> LoginAsync(LoginDto login);

        Task<GeneralRetDto> AssignRole(AssignRoleDto assignRole);
        Task<GeneralRetDto> CreateRole(CreateRoleDto createRole);
        Task<List<IdentityRole>> GetRoles();





    }
}