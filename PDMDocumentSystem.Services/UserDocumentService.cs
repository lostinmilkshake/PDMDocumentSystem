using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDMDocumentSystem.Data.Models;
using PDMDocumentSystem.Services.Interfaces;

namespace PDMDocumentSystem.Services;

public class UserDocumentService : IUserDocumentService
{
    private readonly IGenericRepository<UserDocument> _userDocumentRepository;

    public UserDocumentService(IGenericRepository<UserDocument> userDocumentRepository)
    {
        _userDocumentRepository = userDocumentRepository;
    }

    public async Task<IEnumerable<UserDocument>> GetAllUserDocumentsAsync()
    {
        return await _userDocumentRepository.GetAllAsync();
    }

    public async Task<UserDocument> GetUserDocumentByIdAsync(Guid id)
    {
        return await _userDocumentRepository.GetByIdAsync(id);
    }

    public async Task CreateUserDocumentAsync(UserDocument userDocument)
    {
        await _userDocumentRepository.CreateAsync(userDocument);
    }    

    public async Task UpdateUserDocumentAsync(UserDocument userDocument)
    {
        await _userDocumentRepository.UpdateAsync(userDocument);
    }

    public async Task DeleteUserDocumentAsync(UserDocument userDocument)
    {
        await _userDocumentRepository.DeleteAsync(userDocument);
    }
}
