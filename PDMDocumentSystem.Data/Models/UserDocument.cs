namespace PDMDocumentSystem.Data.Models;

public class UserDocument
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid DocumentId { get; set; }
    public Document Document { get; set; }

    public bool Subscribed { get; set; }
}