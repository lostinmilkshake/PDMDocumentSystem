using Microsoft.EntityFrameworkCore;
using PDMDocumentSystem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbContext, PDMDocumentContext>();


builder.Services.AddDbContextFactory<PDMDocumentContext>((sp, options) =>
{
    options.UseCosmos(connectionString: builder.Configuration.GetConnectionString("PdmDocumentDB"),
        databaseName: "pdmdocumentdb");
}).AddScoped<PDMDocumentContext>(p => p.GetRequiredService<IDbContextFactory<PDMDocumentContext>>().CreateDbContext());

builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IGenericRepository<Document>, GenericRepository<Document>>();
builder.Services.AddScoped<IGenericRepository<UserDocument>, GenericRepository<UserDocument>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
