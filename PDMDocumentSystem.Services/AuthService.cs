using PDMDocumentSystem.Data.Models;

namespace PDMDocumentSystem.Services;

public class AuthService : IAuthService
{
    private readonly IGenericRepository<User> _userRepository;
    
    public AuthService(IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> AuthenticateAsync(string email)
    {
        var user = await _userRepository.GetByConditionAsync(u => u.Email == email);

        if (user.Count() != 0)
        {
            return user.FirstOrDefault();
        }

        var newUser = new User
        {
            Email = email,
            Name = email.Split('@')[0],
            Password = "password"
        };
        await _userRepository.CreateAsync(newUser);

        return newUser;
    }
}