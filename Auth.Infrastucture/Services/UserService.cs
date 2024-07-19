using Auth.Application.Services;
using Auth.Domain.Dtos;

public class UserService : IUserService
{
    public Task<bool> CreateUserAsync(CreateUserDto user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(UserDto user)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetUserByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUserAsync(UserDto user)
    {
        throw new NotImplementedException();
    }
}