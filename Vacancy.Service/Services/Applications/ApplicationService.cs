using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vacancy.Data.IRepositories.Applicants;
using Vacancy.Data.IRepositories.Applications;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Data.IRepositories.Vacancies;
using Vacancy.Domain.Entities.Applicants;
using Vacancy.Domain.Entities.Applications;
using Vacancy.Domain.Entities.Vacancies;
using Vacancy.Domain.Enums.Applications;
using Vacancy.Service.Configurations;
using Vacancy.Service.DTOs.ApplicationDtos;
using Vacancy.Service.DTOs.VacancyDtos;
using Vacancy.Service.Exceptions;
using Vacancy.Service.Extensions;
using Vacancy.Service.Helpers;
using Vacancy.Service.Interfaces.Applicatoins;
using Vacancy.Service.Interfaces.Assetss;
using Vacancy.Service.Services.Vacancies.Extentions;

namespace Vacancy.Service.Services.Applications;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepo;
    private readonly IApplicantRepository _applicantRepo;
    private readonly IVakancyRepository _vakancyRepo;
    private readonly IAssetsService _assetsService;
    private readonly IUserRepository _userRepo;
    private readonly IMapper _mapper;

    public ApplicationService(IApplicationRepository applicationRepo, IMapper mapper, IUserRepository userRepo, IAssetsService assetsService, IApplicantRepository applicantRepo, IVakancyRepository vakancyRepo)
    {
        _applicationRepo = applicationRepo;
        _mapper = mapper;
        _userRepo = userRepo;
        _assetsService = assetsService;
        _applicantRepo = applicantRepo;
        _vakancyRepo = vakancyRepo;
    }

    public async Task<ApplicationGetDto> ChangeApplicationStatusAsync(ApplicationStatusUpdateDto dto)
    {
        var existingApplication = await _applicationRepo.GetByIdAsync(dto.id);

        if (existingApplication == null)
            throw new VacancyException(404, "Application not found");

        existingApplication.Status = dto.Status;

        existingApplication = await _applicationRepo.UpdateAsync(existingApplication);

        return _mapper.Map<ApplicationGetDto>(existingApplication);
    }

    public async Task<ApplicationGetDto> CreateApplicatoinAsync(int userId, ApplicationCreateDto createDto)
    {
        var user = await _userRepo.GetByIdAsync(userId);

        if (user == null)
            throw new VacancyException(404, "User not found");

        var vacancy = await _vakancyRepo.GetByIdAsync(createDto.VakancyId);

        if (vacancy == null)
            throw new VacancyException(404, "Vacancy not found");

        var existingApplication = await _applicationRepo
            .GetAllAsQueryable()
            .Where(x => x.VakancyId == createDto.VakancyId)
            .FirstOrDefaultAsync();

        if (existingApplication != null)
            throw new VacancyException(400, "You have already applied for this Vacancy");

        var existingApplicant = await _applicantRepo
            .GetAllAsQueryable()
            .Where(x => x.UserId == userId).FirstOrDefaultAsync();

        Applicant newApplicant = new();
        bool isNew = false;

        if(existingApplicant == null)
        {
            isNew = true;
             newApplicant = new()
            {
                FullName = createDto.FullName,
                Address = createDto.Address,
                Email = createDto.Email,
                PhoneNumber = createDto.PhoneNumber,
                User = user,
                UserId = userId,
                PassportPath = _assetsService.CreateAsync(createDto.PassportFile, "Applicatoins/Passports").Result.Path
            };
            newApplicant = await _applicantRepo.CreateAsync(newApplicant);
        }


        Application newApplication = new()
        {
            Applicant = existingApplicant ?? newApplicant,
            ApplicantId = isNew ? newApplicant.Id : existingApplicant.Id,
            Vakancy = vacancy,
            VakancyId = vacancy.Id
        };

        newApplication = await _applicationRepo.CreateAsync(newApplication);

        return _mapper.Map<ApplicationGetDto>(newApplication);
    }

    public async Task<bool> DeleteApplicationAsync(int id)
    {
        var existingApplication = await _applicationRepo.GetByIdAsync(id);

        if (existingApplication == null)
            throw new VacancyException(404, "Application not found");

        var isDeleted = await _applicationRepo.DeleteAsync(id);

        return isDeleted;
    }

    public async Task<bool> DeleteApplicationsAsync(ApplicationsDeleteDto dto)
    {
        IList<Application> applications = new List<Application>();

        foreach(int id in dto.Ids)
        {
            var application = await _applicationRepo.GetByIdAsync(id);

            if(application == null)
                throw new VacancyException(404,$"Application not found with id:  {id}");

            applications.Add(application);
        }

        var AreDeleted = await _applicationRepo.DeleteApplicationsAsync(dto.Ids);

        return AreDeleted;
    }

    public async Task<Response<ApplicationGetDto>> GetAllApplicationsAsync(Params @params)
    {
        var allApplicatoins = await _applicationRepo
            .GetAllAsQueryable()
            .ToListAsync();
        var total = allApplicatoins.Count();

        if (@params.Search is not null)
        {
            allApplicatoins =
            allApplicatoins.Where(application =>
                application.Vakancy.Title.ToLower().Contains(@params.Search.ToLower()) ||
                application.Status.ToString().ToLower().Contains(@params.Search.ToLower()) ||
                application.CreatedAt.ToString().ToLower().Contains(@params.Search.ToLower())).ToList();
        }

        Response<Application> response = OrderAllVacanciesForAscAndDesc(@params, allApplicatoins).ToPagedList(@params);

        IList<ApplicationGetDto> applicationGetDtos = _mapper.Map<IList<ApplicationGetDto>>(response.Result);

        List<AppStatus> allStatus = new List<AppStatus>();
        foreach (var status in Enum.GetNames(typeof(Status)))
        {
            Enum.TryParse(status, out Status status1);
            allStatus.Add(new() { Name = status, Code = ((byte)status1).ToString() });
        }
        return new Response<ApplicationGetDto>()
        {
            Result = applicationGetDtos,
            Pagination = response.Pagination,
            Total = total,
            AllStatus = allStatus
        };
    }

    private static IEnumerable<Application>? OrderAllVacanciesForAscAndDesc(Params @params, IEnumerable<Application>? allApplications)
    {
        Dictionary<string, List<string>> propertiesAndValues = @params.Sort.ExtractPropertiesAndValues();

        foreach (var (property, values) in propertiesAndValues)
        {
            if (values.Count > 0)
            {
                allApplications = property.ToLower() switch
                {
                    "status" => allApplications.Where(v => values.Any(x => x.ToLower() == v.Status.ToString().ToLower())).ToList(),
                    _ => allApplications
                };
            }
        }

        if (@params.Order?.ToLowerInvariant().ExtractPropertyAndOrder().OrderType == "asc")
        {
            allApplications = @params.Order.ToLower().ExtractPropertyAndOrder().Property switch
            {
                "vacancy" => allApplications.OrderBy(x => x.Vakancy.Title),
                "createddate" => allApplications.OrderBy(x => x.CreatedAt),
                "status" => allApplications.OrderBy(x => x.Status)
            };
        }
        else if (@params.Order?.ToLowerInvariant().ExtractPropertyAndOrder().OrderType == "desc")
        {
            allApplications = @params.Order.ToLower().ExtractPropertyAndOrder().Property switch
            {
                "vacancy" => allApplications.OrderByDescending(x => x.Vakancy.Title),
                "createddate" => allApplications.OrderByDescending(x => x.CreatedAt),
                "status" => allApplications.OrderByDescending(x => x.Status)
            };
        }

        return allApplications;
    }

    public async Task<ApplicationGetDto> GetApplicationAsync(int id)
    {
        var existingApplication = await _applicationRepo.GetByIdAsync(id);

        if (existingApplication == null)
            throw new VacancyException(404, "Application not found");

        return _mapper.Map<ApplicationGetDto>(existingApplication);
    }

    public async Task<ApplicationGetDto> UpdateApplicationAsync(int userId, ApplicationUpdateDto updateDto)
    {
        var user = await _userRepo.GetByIdAsync(userId);

        if (user == null)
            throw new VacancyException(404, "User not found");

        if (user.IsApproved != true || user.IsApproved is null)
            throw new VacancyException(403, "Employer is not approved");

        var vacancy = await _vakancyRepo.GetByIdAsync(updateDto.VakancyId);

        if (vacancy == null)
            throw new VacancyException(404, "Vacancy not found");

        var existingApplication = await _applicationRepo.GetByIdAsync(updateDto.Id);

        Applicant? existingApplicant = await _applicantRepo
            .GetAllAsQueryable()
            .FirstOrDefaultAsync(x => x.UserId == userId);

        existingApplicant = _mapper.Map(updateDto, existingApplicant);

        if (updateDto.NewPassportFile != null)
        {
            await _assetsService.DeleteAsync(existingApplicant.PassportPath);
            existingApplicant.PassportPath = _assetsService.CreateAsync(updateDto.NewPassportFile, "Applicatoins/Passports").Result.Path;
        }
        existingApplicant = await _applicantRepo.UpdateAsync(existingApplicant);

        existingApplication.Applicant = existingApplicant;

        existingApplication = await _applicationRepo.UpdateAsync(existingApplication);

        return _mapper.Map<ApplicationGetDto>(existingApplication);
    }
}
