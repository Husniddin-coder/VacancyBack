using Microsoft.AspNetCore.Mvc;
using Vacancy.Api.Attributes;
using Vacancy.Api.Controllers.BaseControllers;
using Vacancy.Domain.Enums.Users;
using Vacancy.Service.Configurations;
using Vacancy.Service.DTOs.VacancyDtos;
using Vacancy.Service.Interfaces.Vacancies;

namespace Vacancy.Api.Controllers.Vacancies;

public class VacancyController : BaseController
{
    private readonly IVakancyService _vacancyService;

    public VacancyController(IVakancyService vacancyService)
    {
        _vacancyService = vacancyService;
    }

    [HttpGet("[action]"), PermissionAuthorize(UserPermission.ViewVacancies)]
    public async Task<IActionResult> GetAllVacancies([FromQuery] Params @params)
        => Ok(await _vacancyService.GetAllVacanciesAsync(this.UserId, @params));

    [HttpGet("[action]"), PermissionAuthorize(UserPermission.ViewVacancy)]
    public async Task<IActionResult> GetVacancy(int id)
        => Ok(await _vacancyService.GetVacancyByIdAsync(this.UserId, id));

    [HttpPost("[action]"), PermissionAuthorize(UserPermission.CreateVacancy)]
    public async Task<IActionResult> CreateVacancy(VakancyCreateDto createDto)
        => Ok(await _vacancyService.CreateVacancyAsync(this.UserId, createDto));

    [HttpPut("[action]"), PermissionAuthorize(UserPermission.UpdateVacancy)]
    public async Task<IActionResult> UpdateVacancy(VakancyUpdateDto updateDto)
        => Ok(await _vacancyService.UpdateVacancyAsync(this.UserId,updateDto));

    [HttpDelete("[action]"), PermissionAuthorize(UserPermission.DeleteVacancy)]
    public async Task<IActionResult> DeleteVacancy(int id)
        => Ok(await _vacancyService.DeleteVacancyAsync(id));
}
