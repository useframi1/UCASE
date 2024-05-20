using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UniversitiesController : BaseApiController
{
    private readonly IUniversityRepository _universityRepository;

    public UniversitiesController(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UniversityDto>>> GetUniversities()
    {
        var universities = await _universityRepository.GetUniversitiesAsync();

        return Ok(universities);
    }

    [HttpGet("{uniName}")]
    public async Task<ActionResult<UniversityDto>> GetUniversities(string uniName)
    {
        return await _universityRepository.GetUniversityAsync(uniName);
    }
}
