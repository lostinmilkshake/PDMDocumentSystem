using Microsoft.AspNetCore.Mvc;

namespace PDMDocumentSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IGenericRepository<Document> _documentRepository;

    public DocumentController(IGenericRepository<Document> documentRepository)
    {
        _documentRepository = documentRepository;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Document?>> GetAllDocuments()
    {
        return await _documentRepository.GetAllAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<Document?> GetDocumentById(Guid id)
    {
        return await _documentRepository.GetByIdAsync(id);
    }
    
    [HttpPost]
    public async Task CreateDocument(Document document)
    {
        await _documentRepository.CreateAsync(document);
    }
    
    [HttpPut]
    public async Task UpdateDocument(Document document)
    {
        await _documentRepository.UpdateAsync(document);
    }
    
    [HttpDelete("{id}")]
    public async Task DeleteDocument(Guid id)
    {
        var document = await _documentRepository.GetByIdAsync(id);
        await _documentRepository.DeleteAsync(document);
    }
}