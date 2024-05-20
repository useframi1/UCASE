using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IApplicationRepository
{
    Task<bool> AddAsync(string email);
    Task<bool> AddUniversityChoiceAsync(UniversityChoiceDto universityChoiceDto);
}
