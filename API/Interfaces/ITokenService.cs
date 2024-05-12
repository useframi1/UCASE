using API.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace API.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
