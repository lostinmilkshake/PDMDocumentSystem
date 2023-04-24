namespace PDMDocumentSystem.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _userRepository;
    
    public UserService(IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }
    
    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
    
    public async Task<User> GetUserByEmailAsync(string email)
    {
        var users = await _userRepository.GetByConditionAsync(u => u.Email == email);
        return users.FirstOrDefault();
    }
    
    public async Task CreateUserAsync(User user)
    {
        await _userRepository.CreateAsync(user);
    }
    
    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }
    
    public async Task DeleteUserAsync(User user)
    {
        await _userRepository.DeleteAsync(user);
    }
}