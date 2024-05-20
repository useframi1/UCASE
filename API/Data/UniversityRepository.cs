using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UniversityRepository : IUniversityRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UniversityRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UniversityDto>> GetUniversitiesAsync()
    {
        return await _context.Universities
            .ProjectTo<UniversityDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<UniversityDto> GetUniversityAsync(string uniName)
    {
        return await _context.Universities
            .Where(u => u.UniName == uniName)
            .ProjectTo<UniversityDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }
}
