using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ApplicationRepository : IApplicationRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public ApplicationRepository(DataContext context, IMapper mapper, IUserRepository userRepository)
    {
        _context = context;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<bool> AddAsync(string email)
    {
        var application = new Application
        {
            Email = email,
        };

        _context.Applications.Add(application);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddUniversityChoiceAsync(UniversityChoiceDto universityChoiceDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(universityChoiceDto.Email);

        if (user == null || user.Application == null)
        {
            return false;
        }

        var university = await _context.Universities
            .FirstOrDefaultAsync(u => u.UniName == universityChoiceDto.UniName);

        if (university == null)
        {
            return false;
        }

        user.Application.UniNames.Add(university);

        return await _context.SaveChangesAsync() > 0;
    }

}
