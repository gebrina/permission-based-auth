using Auth.Application.Services;
using Auth.Domain.Dtos;
using Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CreateUserAsync(CreateUserDto user)
    {
        ApplicationUser appUser = new ApplicationUser
        {
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Profession = user.Profession
        };

        IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
        if (result.Succeeded) return true;

        return false;
    }

    public async Task<bool> DeleteUserAsync(UserDto user)
    {
        var appUser = await _userManager.FindByIdAsync(user.Id);
        if (appUser == null) return false;
        await _userManager.DeleteAsync(appUser);
        return true;
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        var appUser = await _userManager.FindByIdAsync(id);
        UserDto userDto = new UserDto();
        if (appUser != null && appUser.Email != null && appUser.UserName != null)
        {
            userDto.Id = appUser.Id;
            userDto.UserName = appUser.UserName;
            userDto.Email = appUser.Email;
            userDto.Profession = appUser.Profession;
            userDto.FirstName = appUser.FirstName;
            userDto.LastName = appUser.LastName;
        }

        return userDto;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var appUsers = await _userManager.Users.ToListAsync();
        var users = appUsers.Select(appUser => new UserDto
        {
            Id = appUser.Id,
            UserName = appUser.UserName ?? "",
            Email = appUser.Email ?? "",
            Profession = appUser.Profession,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
        });

        return users;
    }

    public async Task<bool> UpdateUserAsync(UserDto userDto)
    {
        var appUser = await _userManager.FindByIdAsync(userDto.Id);
        if (appUser == null) return false;

        appUser.Email = userDto.Email;
        appUser.UserName = userDto.UserName;
        appUser.FirstName = userDto.FirstName;
        appUser.LastName = userDto.LastName;
        appUser.Profession = userDto.Profession;
        var result = await _userManager.UpdateAsync(appUser);
        if (result.Succeeded) return true;
        return false;
    }
}