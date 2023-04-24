using PDMDocumentSystem.Data.Models;

namespace PDMDocumentSystem.Services.Interfaces;

public interface IUserDocumentService
{
    Task CreateUserDocumentAsync(UserDocument userDocument);
    Task DeleteUserDocumentAsync(UserDocument userDocument);
    Task<IEnumerable<UserDocument>> GetAllUserDocumentsAsync();
    Task<UserDocument?> GetUserDocumentByUserIdAndDocumentIdAsync(Guid userId, Guid documentId);
    Task<UserDocument> GetUserDocumentByIdAsync(Guid id);
    Task UpdateUserDocumentAsync(UserDocument userDocument);
}