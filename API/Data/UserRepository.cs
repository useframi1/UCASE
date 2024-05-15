using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MemberDto> GetMemberAsync(string email)
    {
        return await _context.Users
            .Where(u => u.Email == email)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .Include(x => x.FavoriteUniversities)
            .Include(x => x.PreferredIndustries)
            .Include(x => x.PreferredSubjects)
            .SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users
            .Include(x => x.FavoriteUniversities)
            .Include(x => x.PreferredIndustries)
            .Include(x => x.PreferredSubjects)
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }
}
