
using Auth.Domain.Dtos;

namespace Auth.Application.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task<UserDto> GetUserByIdAsync(string id);
    Task<bool> CreateUserAsync(CreateUserDto user);
    Task<bool> UpdateUserAsync(UserDto user);
    Task<bool> DeleteUserAsync(UserDto user);
}