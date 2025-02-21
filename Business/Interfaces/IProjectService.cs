using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

    public interface IProjectService
    {
        Task<Project?> CreateProjectAsync(ProjectRegistrationForm form);
        Task<Project?> GetProjectByIdAsync(int id);
        Task<IEnumerable<Project?>> GetAllProjectsAsync();
        Task<bool> UpdateProjectAsync(int id, ProjectUpdateForm form);
        Task<bool> DeleteProjectAsync(int id);
    }

