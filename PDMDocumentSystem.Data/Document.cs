namespace PDMDocumentSystem;

public class Document
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public User Author { get; set; }
    public string PathToFile { get; set; }
}

 