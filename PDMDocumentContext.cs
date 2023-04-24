using Microsoft.EntityFrameworkCore;

namespace PDMDocumentSystem;

public class PDMDocumentContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<UserDocument> UserDocuments { get; set; }

    public PDMDocumentContext()
    {
        
    }
    
    public PDMDocumentContext(DbContextOptions<PDMDocumentContext> options) : base(options)
    {
        
    }
}