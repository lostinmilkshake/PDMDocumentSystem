namespace PDMDocumentSystem.Services;

public interface IDocumentService
{
    Task<IEnumerable<Document>> GetAllDocumentsAsync();
    Task<Document> GetDocumentByIdAsync(Guid id);
    Task CreateDocumentAsync(Document document);
    Task UpdateDocumentAsync(Document document);
    Task DeleteDocumentAsync(Document document);
}