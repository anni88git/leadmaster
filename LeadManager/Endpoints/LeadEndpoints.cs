using LeadManager.Data;
using LeadManager.DTOs;
using LeadManager.Filters;
using LeadManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadManager.Endpoints;

public static class LeadEndpoints
{
    public static void MapLeadEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/leads")
                       .WithTags("Leads")
                       .WithOpenApi();

        group.MapGet("/", async (LeadDbContext db) =>
        {
            var leads = await db.Leads
                .Select(l => new LeadDto(l.Id, l.Name, l.Email, l.Status))
                .ToListAsync();
            
            return Results.Ok(leads);
        })
        .WithName("GetLeads")
        .Produces<List<LeadDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id:int}", async (int id, LeadDbContext db) =>
        {
            var lead = await db.Leads.FindAsync(id);
            if (lead is null) return Results.NotFound();

            var dto = new LeadDto(lead.Id, lead.Name, lead.Email, lead.Status);
            return Results.Ok(dto);
        })
        .WithName("GetLeadById")
        .Produces<LeadDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateLeadDto dto, LeadDbContext db) =>
        {
            var lead = new Lead
            {
                Name = dto.Name,
                Email = dto.Email,
                Status = string.IsNullOrWhiteSpace(dto.Status) ? "New" : dto.Status
            };

            db.Leads.Add(lead);
            await db.SaveChangesAsync();

            var resultDto = new LeadDto(lead.Id, lead.Name, lead.Email, lead.Status);
            return Results.Created($"/api/leads/{lead.Id}", resultDto);
        })
        .AddEndpointFilter<ValidationFilter<CreateLeadDto>>()
        .WithName("CreateLead")
        .Produces<LeadDto>(StatusCodes.Status201Created)
        .ProducesValidationProblem();

        group.MapPut("/{id:int}", async (int id, UpdateLeadDto dto, LeadDbContext db) =>
        {
            var lead = await db.Leads.FindAsync(id);
            if (lead is null) return Results.NotFound();

            lead.Name = dto.Name;
            lead.Email = dto.Email;
            lead.Status = string.IsNullOrWhiteSpace(dto.Status) ? lead.Status : dto.Status;

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .AddEndpointFilter<ValidationFilter<UpdateLeadDto>>()
        .WithName("UpdateLead")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();

        group.MapDelete("/{id:int}", async (int id, LeadDbContext db) =>
        {
            var lead = await db.Leads.FindAsync(id);
            if (lead is null) return Results.NotFound();

            db.Leads.Remove(lead);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("DeleteLead")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
