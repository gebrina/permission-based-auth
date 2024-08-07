using Auth.Domain.Dtos;
using Auth.Domain.Entities;

namespace Auth.Application.Repository;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetUsersAsync(PagingFilterRequest request);
    Task<UserDto> GetUserByIdAsync(string id);
    Task<ApplicationUser> GetUserByEmailAsync(string email);
    Task<bool> CreateUserAsync(CreateUserDto user);
    Task<bool> UpdateUserAsync(UserDto user);
    Task<bool> DeleteUserAsync(UserDto user);
}