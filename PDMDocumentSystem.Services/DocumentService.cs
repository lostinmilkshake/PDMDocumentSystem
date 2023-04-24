namespace PDMDocumentSystem.Services;

public class DocumentService : IDocumentService
{
    private readonly IGenericRepository<Document> _documentRepository;
    
    public DocumentService(IGenericRepository<Document> documentRepository)
    {
        _documentRepository = documentRepository;
    }
    
    public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
    {
        return await _documentRepository.GetAllAsync();
    }
    
    public async Task<Document> GetDocumentByIdAsync(Guid id)
    {
        return await _documentRepository.GetByIdAsync(id);
    }

    public async Task CreateDocumentAsync(Document document)
    {
        await _documentRepository.CreateAsync(document);
    }
    
    public async Task UpdateDocumentAsync(Document document)
    {
        await _documentRepository.UpdateAsync(document);
    }
    
    public async Task DeleteDocumentAsync(Document document)
    {
        await _documentRepository.DeleteAsync(document);
    }
}