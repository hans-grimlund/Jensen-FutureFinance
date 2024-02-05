using FutureFinance.Domain;

namespace FutureFinance.Core;

public interface IAuthorizationService
{
    string GenerateToken(UserEntity user);
}
