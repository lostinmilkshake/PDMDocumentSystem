using PDMDocumentSystem.Data.Models;

namespace PDMDocumentSystem.Services;

public interface IAuthService
{
    Task<User> AuthenticateAsync(string email);
}