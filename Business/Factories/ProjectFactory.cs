using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;


    public static class ProjectFactory
    {
    public static ProjectRegistrationForm CreateRegistrationForm() => new();
    public static ProjectUpdateForm CreateUpdateForm() => new();


    public static ProjectEntity? Create(ProjectRegistrationForm form)
    {

        if (form == null) return null;


        return new ProjectEntity
        {
            Title = form.Title,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            CustomerId = form.CustomerId,
            StatusId = form.StatusId,
            UserId = form.UserId,
            ProductId = form.ProductId

        };
    }

        public static Project? Create(ProjectEntity entity) => entity == null ? null : new()
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CustomerId = entity.CustomerId,
            StatusId = entity.StatusId,
            UserId = entity.UserId,
            ProductId = entity.ProductId,
            CustomerName = $"{entity.Customer.FirstName}, {entity.Customer.LastName}",
            StatusName = entity.Status.StatusName,
            UserName = $"{entity.User.FirstName}, {entity.User.LastName}",
            ProductName = entity.Product.ProductName
        };

        public static ProjectUpdateForm Create(Project Project) => new()
        {
            Id = Project.Id,
            Title = Project.Title,
            Description = Project.Description,
            StartDate = Project.StartDate,
            EndDate = Project.EndDate,
            CustomerId = Project.CustomerId,
            StatusId = Project.StatusId,
            UserId = Project.UserId,
            ProductId = Project.ProductId
        };
        public static ProjectEntity Create(ProjectEntity ProjectEntity, ProjectUpdateForm form) => new()
        {
            Id = ProjectEntity.Id,
            Title = form.Title,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            CustomerId = form.CustomerId,
            StatusId = form.StatusId,
            UserId = form.UserId,
            ProductId = form.ProductId
        };
    }

