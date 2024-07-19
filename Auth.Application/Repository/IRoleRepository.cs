using Auth.Domain.Dtos;

namespace Auth.Application.Repository;

public interface IRoleRepository
{
    Task<IEnumerable<RoleDto>> GetUsersAsync();
    Task<RoleDto> GetUserByIdAsync(string id);
    Task<bool> CreateUserAsync(CreateRoleDto user);
    Task<bool> UpdateUserAsync(RoleDto user);
    Task<bool> DeleteUserAsync(RoleDto user);
}