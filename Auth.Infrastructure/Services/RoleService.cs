using Auth.Application.Repository;
using Auth.Application.Services;
using Auth.Domain.Dtos;

namespace Auth.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepo;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepo = roleRepository;
    }

    public async Task<bool> CreateRoleAsync(CreateRoleDto role)
    {
        return await _roleRepo.CreateRoleAsync(role);
    }

    public async Task<bool> DeleteRoleAsync(RoleDto role)
    {
        return await _roleRepo.DeleteRoleAsync(role);
    }

    public async Task<RoleDto> GetRoleByIdAsync(string id)
    {
        return await _roleRepo.GetRoleByIdAsync(id);
    }

    public async Task<IEnumerable<RoleDto>> GetRolesAsync()
    {
        return await _roleRepo.GetRolesAsync();
    }

    public async Task<(string, bool)> UpdateRoleAsync(RoleDto role)
    {
        return await _roleRepo.UpdateRoleAsync(role);
    }

    public async Task<bool> CheckRoleExistanceAsync(string roleName)
    {
        return await _roleRepo.CheckRoleExistanceAsync(roleName);
    }
}