using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class ApplicationController : BaseApiController
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationController(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    [HttpGet("addApplication")]
    public async Task<ActionResult<bool>> AddApplication(string email)
    {
        return await _applicationRepository.AddAsync(email);
    }

    [HttpPost("addUniversityChoice")]
    public async Task<ActionResult<bool>> AddUniversityChoice(UniversityChoiceDto universityChoiceDto)
    {
        return await _applicationRepository.AddUniversityChoiceAsync(universityChoiceDto);
    }
}
