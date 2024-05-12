﻿using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Email)) return BadRequest("Account already exists");

        using var hmac = new HMACSHA512();

        var user = new User
        {
            Email = registerDto.Email.ToLower(),
            Passwordhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            Passwordsalt = hmac.Key,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _context.Users.FindAsync(loginDto.Email);

        if (user == null) return Unauthorized("Invalid email");

        using var hmac = new HMACSHA512(user.Passwordsalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.Passwordhash[i]) return Unauthorized("Invalid password");
        }

        return new UserDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = _tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string Email)
    {
        return await _context.Users.AnyAsync(u => u.Email == Email.ToLower());
    }
}
