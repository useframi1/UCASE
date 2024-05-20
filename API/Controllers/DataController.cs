using API.DTOs;
using API.Entities;
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

    [HttpGet("subjects")]
    public async Task<ActionResult<IEnumerable<string>>> GetSubjects()
    {
        var subjects = await _dataRepository.GetSubjectsAsync();

        return Ok(subjects);
    }

    [HttpGet("industries")]
    public async Task<ActionResult<IEnumerable<string>>> GetIndustries()
    {
        var industries = await _dataRepository.GetIndustriesAsync();

        return Ok(industries);
    }

    [HttpGet("universityNames")]
    public async Task<ActionResult<IEnumerable<string>>> GetUniversityNames()
    {
        var universities = await _dataRepository.GetUniversityNamesAsync();

        return Ok(universities);
    }
}
