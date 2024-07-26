using Auth.Domain.Dtos;

namespace Auth.Application.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetRolesAsync();
    Task<RoleDto> GetRoleByIdAsync(string id);
    Task<bool> CreateRoleAsync(CreateRoleDto role);
    Task<bool> CheckRoleExistanceAsync(string roleName);
    Task<(string, bool)> UpdateRoleAsync(RoleDto role);
    Task<bool> DeleteRoleAsync(RoleDto role);
}