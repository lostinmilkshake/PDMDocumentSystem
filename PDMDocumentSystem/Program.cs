using Microsoft.EntityFrameworkCore;
using PDMDocumentSystem;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using PDMDocumentSystem.Services;
using PDMDocumentSystem.Services.Interfaces;
using PDMDocumentSystem.Data.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options =>
    {
        builder.Configuration.Bind("AzureAd", options);
        options.TokenValidationParameters.NameClaimType = "name";
    }, options => { builder.Configuration.Bind("AzureAd", options); });

builder.Services.AddAuthorization(config =>
{
    
});


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
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
