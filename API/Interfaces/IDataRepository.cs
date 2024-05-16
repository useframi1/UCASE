using API.DTOs;

namespace API.Interfaces;

public interface IDataRepository
{
    Task<IEnumerable<GovernorateDto>> GetGovernoratesAsync();
}
