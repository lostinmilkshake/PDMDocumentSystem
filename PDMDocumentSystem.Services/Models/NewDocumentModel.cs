using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using PDMDocumentSystem.Data.Models;

namespace PDMDocumentSystem.Services.Models;

public class NewDocumentModel
{
    [AllowNull]
    public Document Document { get; set; }
    public IFormFile File { get; set; }
}
