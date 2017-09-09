using Infrastructure.Dto;

namespace Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(System.Guid userId);
    }
}