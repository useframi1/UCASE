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

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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

    [HttpPut("updateDetails")]
    public async Task<ActionResult<bool>> UpdateUserDetails(DetailsDto detailsDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(detailsDto.Email);

        user.Dob = detailsDto.Dob;
        user.Gender = detailsDto.Gender;
        user.PhoneNo = detailsDto.PhoneNo;
        user.Nationality = detailsDto.Nationality;
        user.StartUni = detailsDto.StartUni;

        _userRepository.Update(user);

        return await _userRepository.SaveAllAsync();
    }

    [HttpPut("updateAddress")]
    public async Task<ActionResult<bool>> UpdateUserAddress(AddressDto addressDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(addressDto.Email);

        user.GovName = addressDto.GovName;
        user.Area = addressDto.Area;
        user.AddressLine1 = addressDto.AddressLine1;
        user.AddressLine2 = addressDto.AddressLine2;

        _userRepository.Update(user);

        return await _userRepository.SaveAllAsync();
    }
}