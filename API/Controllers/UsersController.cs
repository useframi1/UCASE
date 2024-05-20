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

    [HttpPut("updateInterests")]
    public async Task<ActionResult<bool>> UpdateUserInterests(InterestsDto interestsDto)
    {
        return await _userRepository.UpdateFavoriteUniversitiesAsync(interestsDto.Email, interestsDto.Universities)
            && await _userRepository.UpdatePreferredIndustriesAsync(interestsDto.Email, interestsDto.Industries)
            && await _userRepository.UpdatePreferredSubjectsAsync(interestsDto.Email, interestsDto.Subjects);
    }

    [HttpPut("editProfile")]
    public async Task<ActionResult<bool>> EditProfile(ProfileDto profileDto)
    {
        return await _userRepository.EditProfileAsync(profileDto);
    }

    [HttpGet("recommendedUnis")]
    public async Task<ActionResult<IEnumerable<UniversityDto>>> GetRecommendedUnis(string email)
    {
        return Ok(await _userRepository.GetRecommendedUniversitiesAsync(email));
    }

    [HttpPut("updateGuardianInfo")]
    public async Task<ActionResult<bool>> UpdateGuardianInfo(GuardianInfoDto guardianInfoDto)
    {
        return await _userRepository.UpdateGuardianInfoAsync(guardianInfoDto);
    }

    [HttpPut("updateEducation")]
    public async Task<ActionResult<bool>> UpdateEducation(EducationDto educationDto)
    {
        return await _userRepository.UpdateEducationAsync(educationDto);
    }
}