using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IUserService
{
    Task<User?> CreateUserAsync(UserRegistrationForm form);
    Task<User?> GetUserByIdAsync(int id);
    Task<IEnumerable<User?>> GetAllUsersAsync();
    Task<bool> UpdateUserAsync(int id, UserUpdateForm form);
    Task<bool> DeleteUserAsync(int id);
}
