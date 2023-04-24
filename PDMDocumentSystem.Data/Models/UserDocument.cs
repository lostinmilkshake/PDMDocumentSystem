namespace PDMDocumentSystem.Data.Models;

public class UserDocument
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid DocumentId { get; set; }

    public bool Subscribed { get; set; }
}