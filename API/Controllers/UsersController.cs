using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _userRepository.GetMembersAsync();

        return Ok(users);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<MemberDto>> GetUser(string email)
    {
        return await _userRepository.GetMemberAsync(email);
    }

    [AllowAnonymous]
    [HttpPut("updateDetails")]
    public async Task<ActionResult<bool>> UpdateUserDetails(DetailsDto detailsDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(detailsDto.Email);

        user.Dob = detailsDto.Dob;
        user.Gender = detailsDto.Gender;
        user.Phoneno = detailsDto.Phoneno;
        user.Nationality = detailsDto.Nationality;
        user.StartUni = detailsDto.StartUni;

        _userRepository.Update(user);

        return await _userRepository.SaveAllAsync();
    }
}