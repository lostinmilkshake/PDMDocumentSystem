using PDMDocumentSystem.Data.Models;
using PDMDocumentSystem.Services.Models;

namespace PDMDocumentSystem.Services.Interfaces;

public interface IDocumentService
{
    Task<IEnumerable<Document>> GetAllDocumentsAsync();
    Task<Document> GetDocumentByIdAsync(Guid id);
    Task CreateDocumentAsync(Document document);
    Task UpdateDocumentAsync(Document document);
    Task DeleteDocumentAsync(Document document);
    Task<byte[]> DownloadDocumentAsync(Guid id, string blobContainerConnectionString, string containerName);
    Task UploadDocumentAsync(NewDocumentModel documentModel, string blobContainerConnectionString, string containerName);
    Task UpdateUploadedDocumentAsync(NewDocumentModel documentModel, string blobContainerConnectionString,
        string containerName, string? serviceBusConnectionString, string? serviceBusQueue);
}