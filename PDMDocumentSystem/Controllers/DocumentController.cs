using Microsoft.AspNetCore.Mvc;
using PDMDocumentSystem.Data.Models;
using PDMDocumentSystem.Services.Interfaces;
using PDMDocumentSystem.Services.Models;
using System.IO;

namespace PDMDocumentSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private const string BLOB_CONTAINER = "BlobContainer";
    private readonly IDocumentService _documentService;
    private readonly IConfiguration _configuration;

    public DocumentController(IDocumentService documentService, IConfiguration configuration)
    {
        _documentService = documentService;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IEnumerable<Document?>> GetAllDocuments()
    {
        return await _documentService.GetAllDocumentsAsync();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<Document?> GetDocumentById(Guid id)
    {
        return await _documentService.GetDocumentByIdAsync(id);
    }
    
    [HttpPost]
    public async Task CreateDocument(Document document)
    {
        await _documentService.CreateDocumentAsync(document);
    }
    
    [HttpGet("download/{id:guid}")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        var result = await _documentService.DownloadDocumentAsync(id, 
            _configuration.GetConnectionString(BLOB_CONTAINER), 
            _configuration.GetValue<string>(BLOB_CONTAINER));
        return File(result, "application/octet-stream");
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm] NewDocumentModel postRequest)
    {
        await _documentService.UploadDocumentAsync(postRequest, 
            _configuration.GetConnectionString(BLOB_CONTAINER), 
            _configuration.GetValue<string>(BLOB_CONTAINER));
        return Ok();
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