using API.DTOs;

namespace API.Interfaces;

public interface IDataRepository
{
    Task<IEnumerable<GovernorateDto>> GetGovernoratesAsync();
    Task<IEnumerable<string>> GetSubjectsAsync();
    Task<IEnumerable<string>> GetIndustriesAsync();
    Task<IEnumerable<string>> GetUniversityNamesAsync();
}
