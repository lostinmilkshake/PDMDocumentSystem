using Azure.Storage.Blobs;
using PDMDocumentSystem.Data.Models;
using PDMDocumentSystem.Services.Interfaces;
using PDMDocumentSystem.Services.Models;

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

    public async Task<byte[]> DownloadDocumentAsync(Guid id, string blobContainerConnectionString, string containerName)
    {
        var document = await GetDocumentByIdAsync(id);

        var blobContainerClient = new BlobContainerClient(blobContainerConnectionString, containerName);

        var blobClient = blobContainerClient.GetBlobClient(document.PathToFile + document.Title);

        using var memoryStream = new MemoryStream();
        blobClient.DownloadTo(memoryStream);

        return memoryStream.ToArray();
    }

    public async Task UpdateUploadedDocumentAsync(NewDocumentModel documentModel, string blobContainerConnectionString, string containerName)
    {
        await _documentRepository.UpdateAsync(documentModel.Document);
        
        var blobContainerClient = new BlobContainerClient(blobContainerConnectionString, containerName);

        var blobClient = blobContainerClient.GetBlobClient(documentModel.Document.PathToFile + documentModel.Document.Title);
        using var memoryStream = new MemoryStream();
        await documentModel.File.CopyToAsync(memoryStream);

        await blobClient.UploadAsync(memoryStream, overwrite: true);
    }

    public async Task UploadDocumentAsync(NewDocumentModel documentModel, string blobContainerConnectionString, string containerName)
    {
        await CreateDocumentAsync(documentModel.Document);
        

        // Upload file from documentModel to Azure Blob Storage
        var blobContainerClient = new BlobContainerClient(blobContainerConnectionString, containerName);

        var blobClient = blobContainerClient.GetBlobClient(documentModel.Document.PathToFile + documentModel.Document.Title);

        using var memoryStream = new MemoryStream();
        await documentModel.File.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        await blobClient.UploadAsync(memoryStream, overwrite: true);
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