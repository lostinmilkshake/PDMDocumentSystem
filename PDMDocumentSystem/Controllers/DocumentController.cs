using Microsoft.AspNetCore.Mvc;
using PDMDocumentSystem.Services;

namespace PDMDocumentSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Document?>> GetAllDocuments()
    {
        return await _documentService.GetAllDocumentsAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<Document?> GetDocumentById(Guid id)
    {
        return await _documentService.GetDocumentByIdAsync(id);
    }
    
    [HttpPost]
    public async Task CreateDocument(Document document)
    {
        await _documentService.CreateDocumentAsync(document);
    }
    
    [HttpPut]
    public async Task UpdateDocument(Document document)
    {
        await _documentService.UpdateDocumentAsync(document);
    }
    
    [HttpDelete("{id}")]
    public async Task DeleteDocument(Guid id)
    {
        var document = await _documentService.GetDocumentByIdAsync(id);
        await _documentService.DeleteDocumentAsync(document);
    }
}