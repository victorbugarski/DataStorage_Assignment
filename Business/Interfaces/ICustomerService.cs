using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<Customer?> CreateCustomerAsync(CustomerRegistrationForm form);
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<IEnumerable<Customer?>> GetAllCustomersAsync();
    Task<bool> UpdateCustomerAsync(int id, CustomerUpdateForm form);
    Task<bool> DeleteCustomerAsync(int id);
}
