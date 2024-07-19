using Auth.Application.Repository;
using Auth.Application.Services;
using Auth.Domain.Dtos;

namespace Auth.Infrastructure.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly IRoleService _roleService;

    public RoleRepository(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<bool> CreateRoleAsync(CreateRoleDto role)
    {
        return await _roleService.CreateRoleAsync(role);
    }

    public async Task<bool> DeleteRoleAsync(RoleDto role)
    {
        return await _roleService.DeleteRoleAsync(role);
    }

    public async Task<RoleDto> GetRoleByIdAsync(string id)
    {
        return await _roleService.GetRoleByIdAsync(id);
    }

    public async Task<IEnumerable<RoleDto>> GetRolesAsync()
    {
        return await _roleService.GetRolesAsync();
    }

    public async Task<bool> UpdateRoleAsync(RoleDto role)
    {
        return await _roleService.UpdateRoleAsync(role);
    }
}