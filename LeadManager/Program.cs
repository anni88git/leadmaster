using LeadManager.Data;
using LeadManager.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add database context using SQL Server LocalDB connection string
builder.Services.AddDbContext<LeadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LeadDbContext>();
    db.Database.EnsureCreated();
}

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Helper method for basic model validation using DataAnnotations
static bool TryValidate(Lead lead, out List<string> errors)
{
    var context = new ValidationContext(lead);
    var results = new List<ValidationResult>();
    bool isValid = Validator.TryValidateObject(lead, context, results, validateAllProperties: true);
    
    errors = results.Select(r => r.ErrorMessage ?? "Invalid value").ToList();
    return isValid;
}

// Endpoints

// 1. GET /leads - Retrieve all leads
app.MapGet("/leads", async (LeadDbContext db) =>
{
    return Results.Ok(await db.Leads.ToListAsync());
})
.WithName("GetLeads")
.WithOpenApi();

// 2. GET /leads/{id} - Retrieve a specific lead by id
app.MapGet("/leads/{id}", async (int id, LeadDbContext db) =>
{
    var lead = await db.Leads.FindAsync(id);
    return lead is not null ? Results.Ok(lead) : Results.NotFound();
})
.WithName("GetLeadById")
.WithOpenApi();

// 3. POST /leads - Create a new lead
app.MapPost("/leads", async (Lead lead, LeadDbContext db) =>
{
    if (!TryValidate(lead, out var errors))
        return Results.BadRequest(new { Errors = errors });

    if (string.IsNullOrWhiteSpace(lead.Status))
        lead.Status = "New";

    db.Leads.Add(lead);
    await db.SaveChangesAsync();

    return Results.Created($"/leads/{lead.Id}", lead);
})
.WithName("CreateLead")
.WithOpenApi();

// 4. PUT /leads/{id} - Update an existing lead
app.MapPut("/leads/{id}", async (int id, Lead inputLead, LeadDbContext db) =>
{
    if (!TryValidate(inputLead, out var errors))
        return Results.BadRequest(new { Errors = errors });

    var lead = await db.Leads.FindAsync(id);
    if (lead is null)
        return Results.NotFound();

    lead.Name = inputLead.Name;
    lead.Email = inputLead.Email;
    lead.Status = string.IsNullOrWhiteSpace(inputLead.Status) ? lead.Status : inputLead.Status;

    await db.SaveChangesAsync();

    return Results.NoContent();
})
.WithName("UpdateLead")
.WithOpenApi();

// 5. DELETE /leads/{id} - Delete a lead
app.MapDelete("/leads/{id}", async (int id, LeadDbContext db) =>
{
    var lead = await db.Leads.FindAsync(id);
    if (lead is null)
        return Results.NotFound();

    db.Leads.Remove(lead);
    await db.SaveChangesAsync();

    return Results.NoContent();
})
.WithName("DeleteLead")
.WithOpenApi();

app.Run();
