using FluentValidation;
using LeadManager.Data;
using LeadManager.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure Services
builder.Services.AddDbContext<LeadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add validators from the assembly
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "LeadManager API", Version = "v1" });
});

// Configure Problem Details for standardized error responses
builder.Services.AddProblemDetails();

var app = builder.Build();

// 2. Configure Middleware Pipeline
app.UseExceptionHandler(); // Uses standard ProblemDetails for unhandled exceptions
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Auto-create database for development
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<LeadDbContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();

// 3. Map Endpoints
app.MapLeadEndpoints();

app.Run();
