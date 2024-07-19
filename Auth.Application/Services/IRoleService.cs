using Auth.Domain.Dtos;

namespace Auth.Application.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetUsersAsync();
    Task<RoleDto> GetUserByIdAsync(string id);
    Task<bool> CreateUserAsync(CreateRoleDto user);
    Task<bool> UpdateUserAsync(RoleDto user);
    Task<bool> DeleteUserAsync(RoleDto user);
}