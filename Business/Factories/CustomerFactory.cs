using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerRegistrationForm CreateRegistrationForm() => new();
    public static CustomerUpdateForm CreateUpdateForm() => new();


    public static CustomerEntity? Create(CustomerRegistrationForm form) => form == null ? null : new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email
    };

    public static Customer? Create(CustomerEntity entity) => entity == null ? null : new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Email = entity.Email
    };

    public static CustomerUpdateForm Create(Customer Customer) => new()
    {
        Id = Customer.Id,
        FirstName = Customer.FirstName,
        LastName = Customer.LastName,
        Email = Customer.Email
        
    };
    public static CustomerEntity Create(CustomerEntity CustomerEntity, CustomerUpdateForm form) => new()
    {
        Id = CustomerEntity.Id,
        FirstName = CustomerEntity.FirstName,
        LastName = CustomerEntity.LastName,
        Email = CustomerEntity.Email
    };
}
