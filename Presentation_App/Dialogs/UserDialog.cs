using Business.Factories;
using Business.Interfaces;
using Business.Services;
using Presentation_App.Interfaces;

namespace Presentation_App.Dialogs;

public class UserDialog(IUserService userService) : IUserDialog
{
    private readonly IUserService _userService = userService;

    public async Task CreateUserOption()
    {
        Console.Clear();
        Console.WriteLine("#### CREATE USER ####");

        var user = UserFactory.CreateRegistrationForm();
        Console.Write("First Name: ");
        user.FirstName = Console.ReadLine()!;
        Console.Write("Last Name: ");
        user.LastName = Console.ReadLine()!;
        Console.Write("Email: ");
        user.Email = Console.ReadLine()!;


        var result = await _userService.CreateUserAsync(user);
        if (result != null)
            Console.WriteLine("\nUser was created successfully.");
        else
            Console.WriteLine("\nUser was not created.");

        Console.ReadKey();
    }

    public async Task ViewAllUsersOption()
    {
        Console.Clear();
        Console.WriteLine("#### VIEW ALL USERS ####");

        var results = await _userService.GetAllUsersAsync();

        if (results != null && results.Any())
        {
            foreach (var user in results)
                Console.WriteLine($"{user.Id}, {user.FirstName} {user.LastName} <{user.Email}>");
        }
        else
            Console.WriteLine("No users was found.");

        Console.ReadKey();
    }

    public async Task ViewUserByIdOption()
    {
        Console.Clear();
        Console.WriteLine("#### VIEW USER ####");

        Console.Write("User Id: ");
        var userId = Convert.ToInt32(Console.ReadLine())!;

        var user = await _userService.GetUserByIdAsync(userId);
        if (user != null)
            Console.WriteLine($"{user.Id}, {user.FirstName} {user.LastName} <{user.Email}>");
        else
            Console.WriteLine("User was not found.");

        Console.ReadKey();
    }

    public async Task UpdateUserOption()
    {
        Console.Clear();
        Console.WriteLine("#### UPDATE USER ####");

        Console.Write("User Id: ");
        var userId = Convert.ToInt32(Console.ReadLine())!;

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
            Console.WriteLine("User was not found.");
        else
        {
            Console.WriteLine($"Id: {user.Id}");
            Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine("");

            var userUpdateForm = UserFactory.CreateUpdateForm();
            userUpdateForm.Id = user.Id;

            Console.Write("First Name: ");
            var firstName = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(firstName))
                userUpdateForm.FirstName = firstName;

            Console.Write("Last Name: ");
            var lastName = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(lastName))
                userUpdateForm.LastName = lastName;

            Console.Write("Email: ");
            var email = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(email))
                userUpdateForm.Email = email;

            var updatedUser = await _userService.UpdateUserAsync(user.Id, userUpdateForm);
            if (updatedUser != false)
                Console.WriteLine($"{user.Id}, {user.FirstName} {user.LastName} <{user.Email}>");
            else
                Console.WriteLine("Something went wrong!");
        }

        Console.ReadKey();
    }

    public async Task DeleteUserOption()
    {
        Console.Clear();
        Console.WriteLine("#### DELETE USER ####");

        Console.Write("User Id: ");
        var userId = Convert.ToInt32(Console.ReadLine())!;

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            Console.WriteLine("User was not found.");
            return;
        }

        var result = await _userService.DeleteUserAsync(user.Id);
        if (result)
            Console.WriteLine($"User was deleted successfully.");
        else
            Console.WriteLine("Something went wrong!");


        Console.ReadKey();
    }

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("#### MENU OPTIONS ####");
            Console.WriteLine("1. Create New User");
            Console.WriteLine("2. View All Users");
            Console.WriteLine("3. View User");
            Console.WriteLine("4. Update User");
            Console.WriteLine("5. Delete User");
            Console.WriteLine("6. Back to main menu.");
            Console.Write("SELECTED MENU OPTION: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateUserOption();
                    break;

                case "2":
                    await ViewAllUsersOption();
                    break;

                case "3":
                    await ViewUserByIdOption();
                    break;
                case "4":
                    await UpdateUserOption();
                    break;

                case "5":
                    await DeleteUserOption();
                    break;

                case "6":
                    Console.WriteLine("Returning to main menu..");
                    return;
            }
        }
    }
}
