using Business.Factories;
using Business.Interfaces;
using Presentation_App.Interfaces;

namespace Presentation_App.Dialogs;

// Uppbyggnad av CustomerDialog är tagen ifrån övningsuppgift 3 som jag har återanvänt för alla mina dialoger. Och sedan byggt upp efter de olika dialogerna.
public class CustomerDialogs(ICustomerService customerService) : ICustomerDialog
{
    private readonly ICustomerService _customerService = customerService;

    public async Task CreateCustomerOption()
    {
        Console.Clear();
        Console.WriteLine("#### CREATE CUSTOMER ####");

        var customer = CustomerFactory.CreateRegistrationForm();
        Console.Write("First Name: ");
        customer.FirstName = Console.ReadLine()!;
        Console.Write("Last Name: ");
        customer.LastName = Console.ReadLine()!;
        Console.Write("Email: ");
        customer.Email = Console.ReadLine()!;


        var result = await _customerService.CreateCustomerAsync(customer);
        if (result != null)
            Console.WriteLine("\nCustomer was created successfully.");
        else
            Console.WriteLine("\nCustomer was not created.");

        Console.ReadKey();
    }

    public async Task ViewAllCustomersOption()
    {
        Console.Clear();
        Console.WriteLine("#### VIEW ALL CUSTOMERs ####");

        var results = await _customerService.GetAllCustomersAsync();

        if (results != null && results.Any())
        {
            foreach (var customer in results)
                Console.WriteLine($"{customer.Id}, {customer.FirstName} {customer.LastName} <{customer.Email}>");
        }
        else
            Console.WriteLine("No customers was found.");

        Console.ReadKey();
    }

    public async Task ViewCustomerByIdOption()
    {
        Console.Clear();
        Console.WriteLine("#### VIEW CUSTOMER ####");

        Console.Write("Customer Id: ");
        var customerId = Convert.ToInt32(Console.ReadLine())!;

        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        if (customer != null)
            Console.WriteLine($"{customer.Id}, {customer.FirstName} {customer.LastName} <{customer.Email}>");
        else
            Console.WriteLine("Customer was not found.");

        Console.ReadKey();
    }

    public async Task UpdateCustomerOption()
    {
        Console.Clear();
        Console.WriteLine("#### UPDATE CUSTOMER ####");

        Console.Write("Customer Id: ");
        var customerId = Convert.ToInt32(Console.ReadLine())!;

        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        if (customer == null)
            Console.WriteLine("Customer was not found.");
        else
        {
            Console.WriteLine($"Id: {customer.Id}");
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine("");

            var customerUpdateForm = CustomerFactory.CreateUpdateForm();
            customerUpdateForm.Id = customer.Id;

            Console.Write("First Name: ");
            var firstName = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(firstName))
                customerUpdateForm.FirstName = firstName;

            Console.Write("Last Name: ");
            var lastName = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(lastName))
                customerUpdateForm.LastName = lastName;

            Console.Write("Email: ");
            var email = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(email))
                customerUpdateForm.Email = email;

            var updatedCustomer = await _customerService.UpdateCustomerAsync(customer.Id, customerUpdateForm);
            if (updatedCustomer != false)
                Console.WriteLine($"{customer.Id}, {customer.FirstName} {customer.LastName} <{customer.Email}>");
            else
                Console.WriteLine("Something went wrong!");
        }

        Console.ReadKey();
    }

    public async Task DeleteCustomerOption()
    {
        Console.Clear();
        Console.WriteLine("#### DELETE CUSTOMER ####");

        Console.Write("Customer Id: ");
        var customerId = Convert.ToInt32(Console.ReadLine())!;

        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        if (customer == null)
        {
            Console.WriteLine("Customer was not found.");
            return;
        }
        
        var result = await _customerService.DeleteCustomerAsync(customer.Id);
        if (result)
            Console.WriteLine($"Customer was deleted successfully.");
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
            Console.WriteLine("1. Create New Customer");
            Console.WriteLine("2. View All Customers");
            Console.WriteLine("3. View Customer");
            Console.WriteLine("4. Update Customer");
            Console.WriteLine("5. Delete Customer");
            Console.WriteLine("6. Back to main menu.");
            Console.Write("SELECTED MENU OPTION: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateCustomerOption();
                    break;

                case "2":
                    await ViewAllCustomersOption();
                    break;

                case "3":
                    await ViewCustomerByIdOption();
                    break;
                case "4":
                    await UpdateCustomerOption();
                    break;

                case "5":
                    await DeleteCustomerOption();
                    break;

                case "6":
                    Console.WriteLine("Returning to main menu..");
                    return;
            }
        }
    }
}
