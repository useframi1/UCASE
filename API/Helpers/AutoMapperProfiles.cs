using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, MemberDto>();
        CreateMap<FavoriteUniversity, FavoriteUniDto>();
        CreateMap<PreferredIndustry, PreferredIndustryDto>();
        CreateMap<PreferredSubject, PreferredSubjectDto>();
        CreateMap<RegisterDto, User>();
        CreateMap<Governorate, GovernorateDto>();
    }
}
