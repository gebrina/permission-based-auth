using Auth.Application.Services;
using Auth.Domain.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Services;


public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<bool> CreateRoleAsync(CreateRoleDto role)
    {
        IdentityRole appRole = new IdentityRole
        {
            Name = role.Name
        };

        var result = await _roleManager.CreateAsync(appRole);
        if (result.Succeeded) return true;

        return false;
    }

    public async Task<bool> DeleteRoleAsync(RoleDto role)
    {
        var appRole = await _roleManager.FindByIdAsync(role.Id);
        if (appRole == null) return false;

        var result = await _roleManager.DeleteAsync(appRole);
        if (result.Succeeded) return true;
        return false;
    }

    public async Task<RoleDto> GetRoleByIdAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        var roleDto = new RoleDto();
        if (role != null && role.Name != null)
        {
            roleDto.Id = role.Id;
            roleDto.Name = role.Name;
        }
        return roleDto;
    }

    public async Task<IEnumerable<RoleDto>> GetRolesAsync()
    {
        var appRoles = await _roleManager.Roles.ToListAsync();
        var roleDtos = appRoles.Select(appRole => new RoleDto
        {
            Id = appRole.Id,
            Name = appRole.Name ?? ""
        });

        return roleDtos;
    }

    public async Task<(string, bool)> UpdateRoleAsync(RoleDto role)
    {
        var appRole = await _roleManager.FindByIdAsync(role.Id);
        if (appRole == null) return ("Invalid role", false);

        appRole.Name = role.Name;

        var result = await _roleManager.UpdateAsync(appRole);
        if (result.Succeeded) return (string.Empty, true);
        return ("Something went wrong.", false);
    }

    public async Task<bool> CheckRoleExistanceAsync(string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        return roleExist;
    }
}