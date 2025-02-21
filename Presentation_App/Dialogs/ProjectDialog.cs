using System.Globalization;
using Business.Factories;
using Business.Interfaces;
using Business.Services;
using Presentation_App.Interfaces;

namespace Presentation_App.Dialogs;

public class ProjectDialog(IProjectService projectService) : IProjectDialog
{
    private readonly IProjectService _projectService = projectService;

    public async Task CreateProjectOption()
    {
        Console.Clear();
        Console.WriteLine("#### CREATE PROJECT ####");

        var project = ProjectFactory.CreateRegistrationForm();
        Console.Write("Title: ");
        project.Title = Console.ReadLine()!;
        Console.Write("Project Description: ");
        project.Description = Console.ReadLine()!;
        Console.Write("Start Date (yyyy-MM-dd): ");
        var startDateInput = Console.ReadLine()!;
        if (DateTime.TryParse(startDateInput, out var startDate))
        {
            project.StartDate = startDate;
        }
        else
        {
            Console.WriteLine("Invalid Start Date. Please Use format yyyy-MM-dd.");
        }
        Console.Write("End Date (yyyy-MM-dd): ");
        var endDateInput = Console.ReadLine()!;
        if (DateTime.TryParse(endDateInput, out var endDate))
        {
            project.EndDate = endDate;
        }
        else
        {
            Console.WriteLine("Invalid End Date. Please Use format yyyy-MM-dd.");
        }
        Console.Write("Customer ID: ");
        project.CustomerId = Convert.ToInt32(Console.ReadLine())!;
        Console.Write("Status ID: ");
        project.StatusId = Convert.ToInt32(Console.ReadLine())!;
        Console.Write("User ID: ");
        project.UserId = Convert.ToInt32(Console.ReadLine())!;
        Console.Write("Product ID: ");
        project.ProductId = Convert.ToInt32(Console.ReadLine())!;


        var result = await _projectService.CreateProjectAsync(project);
        if (result != null)
            Console.WriteLine("\nProject was created successfully.");
        else
            Console.WriteLine("\nProject was not created.");

        Console.ReadKey();
    }

    public async Task ViewAllProjectsOption()
    {
        Console.Clear();
        Console.WriteLine("#### VIEW ALL PROJECTS ####");

        var results = await _projectService.GetAllProjectsAsync();

        if (results != null && results.Any())
        {
            foreach (var project in results)
                Console.WriteLine($"{project.Title}, {project.Description} {project.StartDate} {project.StartDate} {project.EndDate} {project.CustomerId} {project.StatusId} {project.UserId} {project.ProductId}");
        }
        else
            Console.WriteLine("No projects was found.");

        Console.ReadKey();
    }

    public async Task ViewProjectByIdOption()
    {
        Console.Clear();
        Console.WriteLine("#### VIEW PROJECT ####");

        Console.Write("Project Id: ");
        var projectId = Convert.ToInt32(Console.ReadLine())!;

        var project = await _projectService.GetProjectByIdAsync(projectId);
        if (project != null)
            Console.WriteLine($"{project.Title}, {project.Description} {project.StartDate} {project.EndDate} {project.CustomerId} {project.StatusId} {project.UserId} {project.ProductId}");
        else
            Console.WriteLine("Project was not found.");

        Console.ReadKey();
    }

    public async Task UpdateProjectOption()
    {
        Console.Clear();
        Console.WriteLine("#### UPDATE PROJECT ####");

        Console.Write("Project Id: ");
        var projectId = Convert.ToInt32(Console.ReadLine())!;

        var project = await _projectService.GetProjectByIdAsync(projectId);
        if (project == null)
            Console.WriteLine("Project was not found.");
        else
        {
            Console.WriteLine($"Project Id: {project.Id}");
            Console.WriteLine($"Title: {project.Title}");
            Console.WriteLine($"Project Description: {project.Description}");
            Console.WriteLine($"Start Date: {project.StartDate}");
            Console.WriteLine($"End Date: {project.EndDate}");
            Console.WriteLine($"Customer Id: {project.CustomerId}");
            Console.WriteLine($"Status Id: {project.StatusId}");
            Console.WriteLine($"User Id: {project.UserId}");
            Console.WriteLine($"Product Id: {project.ProductId}");
            Console.WriteLine("");

            var projectUpdateForm = ProjectFactory.CreateUpdateForm();
            projectUpdateForm.Id = project.Id;

            Console.Write("Title: ");
            var title = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(title))
                projectUpdateForm.Title = title;

            Console.Write("Project Description: ");
            var projctDescription = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(projctDescription))
                projectUpdateForm.Description = projctDescription;

            //Både StartDate och EndDate konvertingarna är tagna ifrån Chat GPT, de dom gör är att dom först läser in datum som en string och ser så den inte är tom, efter det så konverterar den från string till DateTime med hjälp av DateTime.TryParse. Skrivs något fel så skickas ett felmeddelande ut till användaren.

            Console.Write("Start Date (yyyy-MM-dd): ");
            var startDateInput = Console.ReadLine()!;

            if (!string.IsNullOrEmpty(startDateInput) && DateTime.TryParse(startDateInput, out DateTime startDate))
            {
                projectUpdateForm.StartDate = startDate;
            }
            else
            {
                Console.WriteLine("Invalid Start Date. Please Use format yyyy-MM-dd.");
            }

            Console.Write("End Date (yyyy-MM-dd): ");
            var endDateInput = Console.ReadLine()!;

            if (!string.IsNullOrEmpty(endDateInput) && DateTime.TryParse(endDateInput, out DateTime endDate))
            {
                projectUpdateForm.EndDate = endDate;
            }
            else
            {
                Console.WriteLine("Invalid End Date. Please Use format yyyy-MM-dd.");
            }

            // Tagit hjälp av Chat GPT med kod för att konvertera från en string till int då mitt ID blir en int.

            Console.Write("Customer Id: ");
            var customerInput = Console.ReadLine()!;
            if (int.TryParse(customerInput, out int customerId))
            {
                projectUpdateForm.CustomerId = customerId;
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a valid ID.");
            }

            Console.Write("Status Id: ");
            var statusInput = Console.ReadLine()!;
            if (int.TryParse(statusInput, out int statusId))
            {
                projectUpdateForm.StatusId = statusId;
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a valid ID.");
            }

            Console.Write("User Id: ");
            var userInput = Console.ReadLine()!;
            if (int.TryParse(userInput, out int userId))
            {
                projectUpdateForm.UserId = userId;
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a valid ID.");
            }

            Console.Write("Product Id: ");
            var productInput = Console.ReadLine()!;
            if (int.TryParse(productInput, out int productId))
            {
                projectUpdateForm.ProductId = productId;
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a valid ID.");
            }

            var updatedProject = await _projectService.UpdateProjectAsync(project.Id, projectUpdateForm);
            if (updatedProject != false)
                Console.WriteLine($"{project.Id} {project.Title} {project.Description} {project.StartDate} {project.EndDate} {project.CustomerId} {project.StatusId} {project.UserId} {project.ProductId}");
            else
                Console.WriteLine("Something went wrong!");
        }

        Console.ReadKey();
    }

    public async Task DeleteProjectOption()
    {
        Console.Clear();
        Console.WriteLine("#### DELETE PROJECT ####");

        Console.Write("Project Id: ");
        var projectId = Convert.ToInt32(Console.ReadLine())!;

        var project = await _projectService.GetProjectByIdAsync(projectId);
        if (project == null)
            Console.WriteLine("Project was not found.");

        var result = await _projectService.DeleteProjectAsync(project.Id);
        if (result)
            Console.WriteLine($"Project was deleted successfully.");
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
            Console.WriteLine("1. Create New Project");
            Console.WriteLine("2. View All Projects");
            Console.WriteLine("3. View Project");
            Console.WriteLine("4. Update Project");
            Console.WriteLine("5. Delete Project");
            Console.WriteLine("6. Back to main menu.");
            Console.Write("SELECTED MENU OPTION: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateProjectOption();
                    break;

                case "2":
                    await ViewAllProjectsOption();
                    break;

                case "3":
                    await ViewProjectByIdOption();
                    break;
                case "4":
                    await UpdateProjectOption();
                    break;

                case "5":
                    await DeleteProjectOption();
                    break;

                case "6":
                    Console.WriteLine("Returning to main menu..");
                    return;
            }
        }
    }
}
