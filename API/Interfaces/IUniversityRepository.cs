using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUniversityRepository
{
    Task<IEnumerable<UniversityDto>> GetUniversitiesAsync();
    Task<UniversityDto> GetUniversityAsync(string uniName);
}
