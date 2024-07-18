using Microsoft.AspNetCore.Mvc;
using Vacancy.Api.Attributes;
using Vacancy.Api.Controllers.BaseControllers;
using Vacancy.Domain.Enums.Users;
using Vacancy.Service.Configurations;
using Vacancy.Service.DTOs.ApplicationDtos;
using Vacancy.Service.Interfaces.Applicatoins;

namespace Vacancy.Api.Controllers.Applications;

public class ApplicationController : BaseController
{
    private readonly IApplicationService _applicationService;

    public ApplicationController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet("[action]"), PermissionAuthorize(UserPermission.ViewApplications)]
    public async Task<IActionResult> GetAllApplications([FromQuery] Params @params)
        => Ok(await _applicationService.GetAllApplicationsAsync(@params));

    [HttpGet("[action]"), PermissionAuthorize(UserPermission.ViewApplication)]
    public async Task<IActionResult> GetApplication(int id)
        => Ok(await _applicationService.GetApplicationAsync(id));

    [HttpPost("[action]"), PermissionAuthorize(UserPermission.CreateApplication)]
    public async Task<IActionResult> CreateApplication(ApplicationCreateDto dto)
        => Ok(await _applicationService.CreateApplicatoinAsync(this.UserId,dto));

    [HttpPut("[action]"), PermissionAuthorize(UserPermission.UpdateApplication)]
    public async Task<IActionResult> UpdateApplication(ApplicationUpdateDto dto)
        => Ok(await _applicationService.UpdateApplicationAsync(this.UserId,dto));

    [HttpDelete("[action]"), PermissionAuthorize(UserPermission.DeleteApplication)]
    public async Task<IActionResult> DeleteApplication(int id)
        => Ok(await _applicationService.DeleteApplicationAsync(id));
    
    [HttpDelete("[action]"), PermissionAuthorize(UserPermission.DeleteApplications)]
    public async Task<IActionResult> DeleteApplications(ApplicationsDeleteDto dto)
        => Ok(await _applicationService.DeleteApplicationsAsync(dto));

    [HttpPut("[action]"), PermissionAuthorize(UserPermission.ChangeApplicationStatus)]
    public async Task<IActionResult> ChangeApplicationStatus(ApplicationStatusUpdateDto dto)
        => Ok(await _applicationService.ChangeApplicationStatusAsync(dto));
}
