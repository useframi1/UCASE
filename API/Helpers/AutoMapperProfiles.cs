using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, MemberDto>()
            .ForMember(dest => dest.FavoriteUniversities, opt => opt.MapFrom(src => src.Universities));
        CreateMap<University, UniversityDto>().ReverseMap();
        CreateMap<PreferredIndustry, PreferredIndustryDto>();
        CreateMap<PreferredSubject, PreferredSubjectDto>();
        CreateMap<RegisterDto, User>();
        CreateMap<Governorate, GovernorateDto>();
        CreateMap<Application, ApplicationDto>()
            .ForMember(dest => dest.UniversityChoices, opt => opt.MapFrom(src => src.UniNames));
        CreateMap<Certificate, CertificateDto>();
    }
}
