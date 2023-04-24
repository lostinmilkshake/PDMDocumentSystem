using System.Diagnostics.CodeAnalysis;

namespace PDMDocumentSystem.Data.Models;

public class Document
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
    public string PathToFile { get; set; }
}

