using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<Project?> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Title == form.Title);
        var entity = ProjectFactory.Create(form);
        await _projectRepository.CreateAsync(entity!);

        return ProjectFactory.Create(projectEntity!);
    }


    public async Task<IEnumerable<Project?>> GetAllProjectsAsync()
    {
        var projectEntities = await _projectRepository.GetAllAsync();
        return projectEntities.Select(ProjectFactory.Create);
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);
        return projectEntity != null ? ProjectFactory.Create(projectEntity!) : null;
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectUpdateForm form)
    {
        if (form == null)
        {
            return false;
        }
        var existingProject = await _projectRepository.GetAsync(x => x.Id == id);
        if (existingProject != null)
        {
            existingProject.Title = form.Title;
            existingProject.Id = form.Id;

            await _projectRepository.UpdateAsync(existingProject);
        }
        return true;
    }
    public async Task<bool> DeleteProjectAsync(int id)
    {

        var existingProject = await _projectRepository.GetAsync(x => x.Id == id);
        if (existingProject != null)
        {
            return false;
        }

        return await _projectRepository.DeleteAsync(x => x.Id == id);
    }
}

