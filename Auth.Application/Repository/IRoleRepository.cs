using Auth.Domain.Dtos;

namespace Auth.Application.Repository;

public interface IRoleRepository
{
    Task<IEnumerable<RoleDto>> GetRolesAsync();
    Task<RoleDto> GetRoleByIdAsync(string id);
    Task<bool> CreateRoleAsync(CreateRoleDto role);
    Task<(string, bool)> UpdateRoleAsync(RoleDto role);
    Task<bool> DeleteRoleAsync(RoleDto role);
}