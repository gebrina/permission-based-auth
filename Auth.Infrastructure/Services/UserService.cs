using Auth.Application.Repository;
using Auth.Application.Services;
using Auth.Domain.Dtos;
using Auth.Domain.Entities;

namespace Auth.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;

    public UserService(IUserRepository userService)
    {
        _userRepo = userService;
    }

    public async Task<bool> CreateUserAsync(CreateUserDto user)
    {
        return await _userRepo.CreateUserAsync(user);
    }

    public async Task<bool> DeleteUserAsync(UserDto user)
    {
        return await _userRepo.DeleteUserAsync(user);
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        return await _userRepo.GetUserByIdAsync(id);
    }

    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        return await _userRepo.GetUserByEmailAsync(email);
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync(PagingFilterRequest request)
    {
        return await _userRepo.GetUsersAsync(request);
    }

    public async Task<bool> UpdateUserAsync(UserDto user)
    {
        return await _userRepo.UpdateUserAsync(user);
    }

    public async Task<IList<string>> GetUserRoleClaimsAsync(UserDto userDto)
    {
        return await _userRepo.GetUserRoleClaimsAsync(userDto);
    }

    public async Task<string> GetEmailConfirmationToken(string id)
    {
        return await _userRepo.GetEmailConfirmationToken(id);
    }
}