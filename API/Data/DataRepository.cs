using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataRepository : IDataRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public DataRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GovernorateDto>> GetGovernoratesAsync()
    {
        return await _context.Governorates
        .ProjectTo<GovernorateDto>(_mapper.ConfigurationProvider)
        .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetIndustriesAsync()
    {
        return await _context.Industries
        .Select(i => i.IndustryName)
        .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetSubjectsAsync()
    {
        return await _context.Subjects
        .Select(s => s.SubjectName)
        .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetUniversityNamesAsync()
    {
        return await _context.Universities
        .Select(u => u.UniName)
        .ToListAsync();
    }
}
