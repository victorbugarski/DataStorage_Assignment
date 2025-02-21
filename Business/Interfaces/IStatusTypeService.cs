using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IStatusTypeService
{

    Task<StatusType?> CreateStatusTypeAsync(StatusTypeRegistrationForm form);
    Task<StatusType?> GetStatusTypeByIdAsync(int id);
    Task<IEnumerable<StatusType?>> GetAllStatusTypesAsync();
    Task<bool> UpdateStatusTypeAsync(int id, StatusTypeUpdateForm form);
    Task<bool> DeleteStatusTypeAsync(int id);
}
