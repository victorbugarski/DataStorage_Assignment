using Business.Factories;
using Business.Interfaces;
using Business.Services;
using Presentation_App.Interfaces;

namespace Presentation_App.Dialogs;

public class StatusTypeDialog(IStatusTypeService statusTypeService) : IStatusTypeDialog
{
    private readonly IStatusTypeService _statusTypeService = statusTypeService;

    public async Task CreateStatusTypeOption()
    {
        Console.Clear();
        Console.WriteLine("#### CREATE STATUSTYPE ####");

        var statusType = StatusTypeFactory.CreateRegistrationForm();
        Console.Write("Status Name: ");
        statusType.StatusName = Console.ReadLine()!;


        var result = await _statusTypeService.CreateStatusTypeAsync(statusType);
        if (result != null)
            Console.WriteLine("\nStatus type was created successfully.");
        else
            Console.WriteLine("\nStatus type was not created.");

        Console.ReadKey();
    }

    public async Task ViewAllStatusesOption()
    {
        Console.Clear();
        Console.WriteLine("#### VIEW ALL STATUSES ####");

        var results = await _statusTypeService.GetAllStatusTypesAsync();

        if (results != null && results.Any())
        {
            foreach (var statusType in results)
                Console.WriteLine($"{statusType.Id}, {statusType.StatusName}");
        }
        else
            Console.WriteLine("No Status was found.");

        Console.ReadKey();
    }

    public async Task ViewStatusTypeByIdOption()
    {
        Console.Clear();
        Console.WriteLine("#### VIEW STATUS TYPE ####");

        Console.Write("Status type Id: ");
        var statusId = Convert.ToInt32(Console.ReadLine())!;

        var statusType = await _statusTypeService.GetStatusTypeByIdAsync(statusId);
        if (statusType != null)
            Console.WriteLine($"{statusType.Id}, {statusType.StatusName}");
        else
            Console.WriteLine("Status type was not found.");

        Console.ReadKey();
    }

    public async Task UpdateStatusTypeOption()
    {
        Console.Clear();
        Console.WriteLine("#### UPDATE STATUS TYPE ####");

        Console.Write("Status type Id: ");
        var statusId = Convert.ToInt32(Console.ReadLine())!;

        var statusType = await _statusTypeService.GetStatusTypeByIdAsync(statusId);
        if (statusType == null)
            Console.WriteLine("Status type was not found.");
        else
        {
            Console.WriteLine($"Id: {statusType.Id}");
            Console.WriteLine($"Status name: {statusType.StatusName}");
            Console.WriteLine("");

            var statusTypeUpdateForm = StatusTypeFactory.CreateUpdateForm();
            statusTypeUpdateForm.Id = statusType.Id;

            Console.Write("Status name: ");
            var statusName = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(statusName))
                statusTypeUpdateForm.StatusName = statusName;

            var updatedStatusType = await _statusTypeService.UpdateStatusTypeAsync(statusType.Id, statusTypeUpdateForm);
            if (updatedStatusType != false)
                Console.WriteLine($"{statusType.Id}, {statusType.StatusName}");
            else
                Console.WriteLine("Something went wrong!");
        }

        Console.ReadKey();
    }

    public async Task DeleteStatusTypeOption()
    {
        Console.Clear();
        Console.WriteLine("#### DELETE STATUS TYPE ####");

        Console.Write("Status type Id: ");
        var statusId = Convert.ToInt32(Console.ReadLine())!;

        var statusType = await _statusTypeService.GetStatusTypeByIdAsync(statusId);
        if (statusType == null)
            Console.WriteLine("Status was not found.");

        var result = await _statusTypeService.DeleteStatusTypeAsync(statusType.Id);
        if (result)
            Console.WriteLine($"Status was deleted successfully.");
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
            Console.WriteLine("1. Create New Status");
            Console.WriteLine("2. View All Statuses");
            Console.WriteLine("3. View Status");
            Console.WriteLine("4. Update Status");
            Console.WriteLine("5. Delete Status");
            Console.WriteLine("6. Back to main menu.");
            Console.Write("SELECTED MENU OPTION: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateStatusTypeOption();
                    break;

                case "2":
                    await ViewAllStatusesOption();
                    break;

                case "3":
                    await ViewStatusTypeByIdOption();
                    break;
                case "4":
                    await UpdateStatusTypeOption();
                    break;

                case "5":
                    await DeleteStatusTypeOption();
                    break;

                case "6":
                    Console.WriteLine("Returning to main menu..");
                    return;
            }
        }
    }
}
