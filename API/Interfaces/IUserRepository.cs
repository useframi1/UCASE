using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(User user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserByEmailAsync(string email);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto> GetMemberAsync(string email);
}
