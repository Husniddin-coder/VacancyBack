using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacancy.Data.IRepositories.Authentication;
using Vacancy.Data.IRepositories.Vacancies;
using Vacancy.Domain.Entities.Authentications;
using Vacancy.Domain.Entities.Vacancies;
using Vacancy.Service.Configurations;
using Vacancy.Service.DTOs.VacancyDtos;
using Vacancy.Service.Exceptions;
using Vacancy.Service.Extensions;
using Vacancy.Service.Helpers;
using Vacancy.Service.Interfaces.Vacancies;
using Vacancy.Service.Services.Vacancies.Extentions;

namespace Vacancy.Service.Services.Vacancies;

public class VakancyService : IVakancyService
{
    private readonly IVakancyRepository _vacancyRepo;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public VakancyService(IVakancyRepository vacancyRepo, IMapper mapper, IUserRepository userRepository)
    {
        _vacancyRepo = vacancyRepo;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<VakancyGetDto> CreateVacancyAsync(int userId, VakancyCreateDto createDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId);

        if (existingUser is null)
            throw new VacancyException(404, "User not found");

        if (existingUser.IsApproved != true || existingUser.IsApproved is null)
            throw new VacancyException(403, "Employer is not approved");

        var newVacancy = _mapper.Map<Vakancy>(createDto);

        newVacancy.User = existingUser;
        newVacancy.UserId = userId;

        newVacancy = await _vacancyRepo.CreateAsync(newVacancy);
        if (newVacancy is null)
            throw new VacancyException(400, "Vacancy creation failed");

        return _mapper.Map<VakancyGetDto>(newVacancy);
    }

    public async Task<bool> DeleteVacancyAsync(int id)
    {
        var existingVacancy = await _vacancyRepo.GetByIdAsync(id);

        if (existingVacancy is null)
            throw new VacancyException(404, "Vacancy not found");

        var isDeleted = await _vacancyRepo.DeleteAsync(id);

        return isDeleted;
    }

    public async Task<Response<VakancyGetDto>> GetAllVacanciesAsync(int UserId, Params @params)
    {
        var allVacancies = await _vacancyRepo
            .GetAllAsQueryable()
            .ToListAsync();   
        var regions = allVacancies.Select(x => x.Region).Distinct().ToList();
        var companies = allVacancies.Select(x => x.Company).Distinct().ToList();
        var total = allVacancies.Count();

        if (@params.Search is not null)
        {
            allVacancies =
            allVacancies.Where(vacancy =>
                vacancy.Company.ToLower().Contains(@params.Search.ToLower()) ||
                vacancy.Region.ToLower().Contains(@params.Search.ToLower()) ||
                vacancy.Title.ToLower().Contains(@params.Search.ToLower()) ||
                vacancy.WorkingDays.ToLower().Contains(@params.Search.ToLower())).ToList();
        }

        Response<Vakancy> response = OrderAllVacanciesForAscAndDesc(@params, allVacancies)
            .ToPagedList(@params);

        IList<VakancyGetDto> vacacnyGetDtos = _mapper.Map<IList<VakancyGetDto>>(response.Result);

        var vacancies = response.Result.ToList();

        for(int i=0; i < vacancies.Count(); i++)
        {
            if (vacancies[i].Applications.Any(x => x.Applicant.UserId == UserId))
            {
                vacacnyGetDtos[i].ApplicationId = vacancies[i].Applications.Where(x => x.VakancyId == vacancies[i].Id)
                    .FirstOrDefault()?.Id;
            }
        }

        return new Response<VakancyGetDto>()
        {
            Result = vacacnyGetDtos.AsEnumerable(),
            Pagination = response.Pagination,
            Regions = regions,
            Companies = companies,
            Total = total
        };
    }

    private static IEnumerable<Vakancy>? OrderAllVacanciesForAscAndDesc(Params @params, IEnumerable<Vakancy>? allVacancies)
    {
        Dictionary<string, List<string>> propertiesAndValues = @params.Sort.ExtractPropertiesAndValues();

        foreach (var (property, values) in propertiesAndValues)
        {
            if (values.Count > 0)
            {
                allVacancies = property.ToLower() switch
                {
                    "company" => allVacancies.Where(v => values.Any(x => x.ToLower() == v.Company.ToLower())).ToList(),
                    "region" => allVacancies.Where(v => values.Any(x => x.ToLower() == v.Region.ToLower())).ToList(),
                    "salary" => allVacancies.Where(v => values.Any(x => decimal.TryParse(x, out decimal value) && value <= v.Salary)).ToList(),
                    _ => allVacancies
                };
            }
        }

        if (@params.Order?.ToLowerInvariant().ExtractPropertyAndOrder().OrderType == "asc")
        {
            allVacancies = @params.Order.ToLower().ExtractPropertyAndOrder().Property switch
            {
                "company" => allVacancies.OrderBy(x => x.Company),
                "region" => allVacancies.OrderBy(x => x.Region),
                "salary" => allVacancies.OrderBy(x => x.Salary)
            };
        }
        else if (@params.Order?.ToLowerInvariant().ExtractPropertyAndOrder().OrderType == "desc")
        {
            allVacancies = @params.Order.ToLower().ExtractPropertyAndOrder().Property switch
            {
                "company" => allVacancies.OrderByDescending(x => x.Company),
                "region" => allVacancies.OrderByDescending(x => x.Region),
                "salary" => allVacancies.OrderByDescending(x => x.Salary)
            };
        }

        return allVacancies;
    }

    public async Task<VakancyGetDto> GetVacancyByIdAsync(int userId, int id)
    {
        var vacancy = await _vacancyRepo.GetByIdAsync(id);

        if (vacancy is null)
            throw new VacancyException(404, "Vacancy not found");

        return _mapper.Map<VakancyGetDto>(vacancy);
    }

    public async Task<VakancyGetDto> UpdateVacancyAsync(int userId, VakancyUpdateDto updateDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId);

        if (existingUser is null)
            throw new VacancyException(404, "User not found");

        if (existingUser.IsApproved != true || existingUser.IsApproved is null)
            throw new VacancyException(403, "Employer is not approved");

        var oldVacancy =await _vacancyRepo.GetByIdAsync(updateDto.Id);

        if (oldVacancy is null)
            throw new VacancyException(404, "Vacancy not found");

        oldVacancy = _mapper.Map(updateDto, oldVacancy);

        var modifiedVacancy = await _vacancyRepo.UpdateAsync(oldVacancy);

        if (modifiedVacancy is null)
            throw new VacancyException(400, "Vacancy modification failed");

        return _mapper.Map<VakancyGetDto>(modifiedVacancy);
    }
}
