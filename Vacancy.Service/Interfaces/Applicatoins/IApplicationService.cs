using Vacancy.Service.Configurations;
using Vacancy.Service.DTOs.ApplicationDtos;
using Vacancy.Service.Helpers;

namespace Vacancy.Service.Interfaces.Applicatoins;

public interface IApplicationService
{
    public Task<ApplicationGetDto> CreateApplicatoinAsync(int userId, ApplicationCreateDto createDto);

    public Task<ApplicationGetDto> UpdateApplicationAsync(int userId, ApplicationUpdateDto updateDto);

    public Task<bool> DeleteApplicationAsync(int id); 
    
    public Task<bool> DeleteApplicationsAsync(ApplicationsDeleteDto dto);

    public Task<ApplicationGetDto> GetApplicationAsync(int id);

    public Task<Response<ApplicationGetDto>> GetAllApplicationsAsync(Params @params);

    public Task<ApplicationGetDto> ChangeApplicationStatusAsync(ApplicationStatusUpdateDto dto);
}
