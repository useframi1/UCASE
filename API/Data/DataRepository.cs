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
}
