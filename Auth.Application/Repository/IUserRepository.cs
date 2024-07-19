
using Auth.Domain.Entities;

namespace Auth.Application.Repository;

public interface IUserRepository
{
    Task<IEnumerable<ApplicationUser>> GetUsersAsync();
    Task<ApplicationUser> GetUserByIdAsync(string id);
    Task<bool> CreateUserAsync(ApplicationUser user);
    Task<bool> UpdateUserAsync(ApplicationUser user);
    Task<bool> DeleteUserAsync(ApplicationUser user);
}