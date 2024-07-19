using Auth.Application.Repository;
using Auth.Application.Services;
using Auth.Domain.Dtos;

namespace Auth.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly IUserService _userService;

    public UserRepository(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> CreateUserAsync(CreateUserDto user)
    {
        return await _userService.CreateUserAsync(user);
    }

    public async Task<bool> DeleteUserAsync(UserDto user)
    {
        return await _userService.DeleteUserAsync(user);
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        return await _userService.GetUserByIdAsync(id);
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        return await _userService.GetUsersAsync();
    }

    public async Task<bool> UpdateUserAsync(UserDto user)
    {
        return await _userService.UpdateUserAsync(user);
    }
}