﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDMDocumentSystem.Data.Models;
using PDMDocumentSystem.Services.Interfaces;

namespace PDMDocumentSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserDocumentController : ControllerBase
{
    private readonly IUserDocumentService _userDocumentService;

    public UserDocumentController(IUserDocumentService userDocumentService)
    {
        _userDocumentService = userDocumentService;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDocument>> GetAllUserDocuments()
    {
        return await _userDocumentService.GetAllUserDocumentsAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<UserDocument> GetUserDocumentById(Guid id)
    {
        return await _userDocumentService.GetUserDocumentByIdAsync(id);
    }
    
    [HttpGet("{userId:guid}/{documentId:guid}")]
    public async Task<UserDocument?> GetUserDocumentById(Guid userId, Guid documentId)
    {
        return await _userDocumentService.GetUserDocumentByUserIdAndDocumentIdAsync(userId, documentId);
    }

    [HttpPost]
    public async Task CreateUserDocument(UserDocument userDocument)
    {
        await _userDocumentService.CreateUserDocumentAsync(userDocument);
    }

    [HttpPut]
    public async Task UpdateUserDocument(UserDocument userDocument)
    {
        await _userDocumentService.UpdateUserDocumentAsync(userDocument);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteUserDocument(Guid id)
    {
        var userDocument = await _userDocumentService.GetUserDocumentByIdAsync(id);
        await _userDocumentService.DeleteUserDocumentAsync(userDocument);
    }
}
