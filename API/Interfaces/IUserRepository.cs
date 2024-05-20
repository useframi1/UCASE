using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(User user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserByEmailAsync(string email);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto> GetMemberAsync(string email);
    Task<bool> UpdatePreferredSubjectsAsync(string email, string[] subjects);
    Task<bool> UpdatePreferredIndustriesAsync(string email, string[] industries);
    Task<bool> UpdateFavoriteUniversitiesAsync(string email, string[] universities);
    Task<bool> EditProfileAsync(ProfileDto profileDto);
    Task<IEnumerable<UniversityDto>> GetRecommendedUniversitiesAsync(string email);
    Task<bool> UpdateGuardianInfoAsync(GuardianInfoDto guardianInfoDto);
    Task<bool> UpdateEducationAsync(EducationDto educationDto);
}
