using Vacancy.Service.Configurations;
using Vacancy.Service.DTOs.VacancyDtos;
using Vacancy.Service.Helpers;

namespace Vacancy.Service.Interfaces.Vacancies;

public interface IVakancyService
{
    public Task<VakancyGetDto> CreateVacancyAsync(int userId, VakancyCreateDto createDto);

    public Task<VakancyGetDto> UpdateVacancyAsync(int userId, VakancyUpdateDto updateDto);

    public Task<VakancyGetDto> GetVacancyByIdAsync(int userId, int id);

    public Task<Response<VakancyGetDto>> GetAllVacanciesAsync(int UserId, Params @params);

    public Task<bool> DeleteVacancyAsync(int id);
}
