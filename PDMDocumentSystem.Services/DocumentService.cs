using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using PDMDocumentSystem.Data.Models;
using PDMDocumentSystem.Services.Interfaces;
using PDMDocumentSystem.Services.Models;

namespace PDMDocumentSystem.Services;

public class DocumentService : IDocumentService
{
    private readonly IGenericRepository<Document> _documentRepository;
    private readonly IGenericRepository<UserDocument> _userDocumentRepository;
    private readonly IGenericRepository<User> _userRepository;

    public DocumentService(IGenericRepository<Document> documentRepository,
        IGenericRepository<UserDocument> userDocumentRepository,
        IGenericRepository<User> userRepository)
    {
        _documentRepository = documentRepository;
        _userDocumentRepository = userDocumentRepository;
        _userRepository = userRepository;
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

    public async Task UpdateUploadedDocumentAsync(NewDocumentModel documentModel, string blobContainerConnectionString,
        string containerName, string? serviceBusConnectionString, string? serviceBusQueue)
    {
        await _documentRepository.UpdateAsync(documentModel.Document);
        
        var blobContainerClient = new BlobContainerClient(blobContainerConnectionString, containerName);

        var blobClient = blobContainerClient.GetBlobClient(documentModel.Document.PathToFile + documentModel.Document.Title);
        using var memoryStream = new MemoryStream();
        await documentModel.File.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        await blobClient.UploadAsync(memoryStream, overwrite: true);

        await SendNotificationToServiceBus(documentModel, serviceBusConnectionString, serviceBusQueue);
    }

    private async Task SendNotificationToServiceBus(NewDocumentModel documentModel, string? serviceBusConnectionString,
        string? serviceBusQueue)
    {
        var serviceBusClient = new ServiceBusClient(serviceBusConnectionString);
        var sender = serviceBusClient.CreateSender(serviceBusQueue);

        var subscribedUsersId =
            await _userDocumentRepository.GetByConditionAsync(ud =>
                ud!.DocumentId == documentModel.Document.Id && ud.Subscribed);
        var subscribedUsersEmails = new NotifySubscribedUserModel
        {
            SubscribedUsersEmails = (await _userRepository.GetByConditionAsync(u => subscribedUsersId.Contains<>(u.Id)))
                .Select(u => u?.Email),
            Document = documentModel.Document
        };

        await sender.SendMessageAsync(new ServiceBusMessage(JsonSerializer.Serialize(subscribedUsersEmails)));
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