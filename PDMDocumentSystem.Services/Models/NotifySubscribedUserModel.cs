using PDMDocumentSystem.Data.Models;

namespace PDMDocumentSystem.Services.Models;

public class NotifySubscribedUserModel
{
    public IEnumerable<string> SubscribedUsersEmails { get; set; }
    public Document Document { get; set; }
}