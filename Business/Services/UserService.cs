using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User?> CreateUserAsync(UserRegistrationForm form)
    {
        var existingUser = await _userRepository.GetAsync(x => x.Email == form.Email);
        if (existingUser != null)
        {
            return null;
        }

        var userEntity = UserFactory.Create(form);
        await _userRepository.CreateAsync(userEntity!);

        return UserFactory.Create(userEntity!);
    }


    public async Task<IEnumerable<User?>> GetAllUsersAsync()
    {
        var userEntities = await _userRepository.GetAllAsync();
        return userEntities.Select(UserFactory.Create);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var userEntity = await _userRepository.GetAsync(x => x.Id == id);
        return userEntity != null ? UserFactory.Create(userEntity) : null;
    }


    // Denna Update är gjord av Chat GPT, koden börjar med att kontrollera ifall ID finns, sedan returnerar koden false om den inte hittar id. I nästa del så hämtas användaren från databasen och skulle den inte hittas så skickas false tillbaka. Och i sista delen så uppdaterar man de fält som finns i min UserEntity, efter det sparas det och till sist så returneras True för att visa att uppdateringen av användaren har lyckats.
    
    public async Task<bool> UpdateUserAsync(int id, UserUpdateForm form)
    {
        if (id <= 0)
        {
            return false;
        }

        var existingUser = await _userRepository.GetAsync(x=> x.Id == id);
        if (existingUser != null)
        {
            existingUser.Id = form.Id;
            existingUser.FirstName = form.FirstName;
            existingUser.LastName = form.LastName;
            existingUser.Email = form.Email;

            await _userRepository.UpdateAsync(existingUser);
        }

        return true;
    }

    // Små delar av Chat GPT
    public async Task<bool> DeleteUserAsync(int id)
    {
        var existingUser = await _userRepository.GetAsync(x => x.Id == id);
        if (existingUser != null)
        {
            return false;
        }

        return await _userRepository.DeleteAsync(x => x.Id == id);
        
    }
}
