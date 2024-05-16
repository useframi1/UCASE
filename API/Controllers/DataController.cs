using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DataController : BaseApiController
{
    private readonly IDataRepository _dataRepository;

    public DataController(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }

    [HttpGet("governorates")]
    public async Task<ActionResult<IEnumerable<GovernorateDto>>> GetGovernorates()
    {
        var governorates = await _dataRepository.GetGovernoratesAsync();

        return Ok(governorates);
    }
}
