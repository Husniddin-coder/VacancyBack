using AutoMapper;
using Vacancy.Domain.Entities.Applicants;
using Vacancy.Domain.Entities.Applications;
using Vacancy.Domain.Entities.Authentications;
using Vacancy.Domain.Entities.Vacancies;
using Vacancy.Service.DTOs.ApplicationDtos;
using Vacancy.Service.DTOs.AuthDtos.PermissionDtos;
using Vacancy.Service.DTOs.AuthDtos.RoleDtos;
using Vacancy.Service.DTOs.AuthDtos.TokenDtos;
using Vacancy.Service.DTOs.AuthDtos.UserDtos;
using Vacancy.Service.DTOs.VacancyDtos;
using Vacancy.Service.Interfaces.Authentication.Tokens;

namespace Vacancy.Service.Mappers;  

public class MappingProfile : Profile
{
    public MappingProfile()
    {


        //Role
        CreateMap<RoleCreateUpdateDto, Role>();
        CreateMap<Role, RoleGetDto>();

        //Permission
        CreateMap<Permission, PermissionGetDto>();

        //Token
        CreateMap<TokenCreateUpdateDto, TokenModel>();
        CreateMap<TokenModel, TokenGetDto>();

        //User
        CreateMap<User, UserResponse>();
        CreateMap<User,UserGetDto>();
        CreateMap<UserUpdateDto, User>();

        //Vacancy
        CreateMap<VakancyCreateDto, Vakancy>();
        CreateMap<VakancyUpdateDto, Vakancy>();
        CreateMap<Vakancy, VakancyGetDto>();

        //Application
        CreateMap<ApplicationUpdateDto, Applicant>();
        CreateMap<Application, ApplicationGetDto>()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.VakancyId, opt => opt.MapFrom(src => src.VakancyId))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Applicant.Email))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Applicant.Address))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Applicant.FullName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Applicant.PhoneNumber))
            .ForMember(dest => dest.VacancyTitle, opt => opt.MapFrom(src => src.Vakancy.Title))
            .ForMember(dest => dest.VacancyCompany, opt => opt.MapFrom(src => src.Vakancy.Company))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.PassportPath, opt => opt.MapFrom(src => src.Applicant.PassportPath));
    }

}
