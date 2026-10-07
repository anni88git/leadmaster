namespace LeadManager.DTOs;

public record LeadDto(int Id, string Name, string Email, string Status);
public record CreateLeadDto(string Name, string Email, string? Status);
public record UpdateLeadDto(string Name, string Email, string? Status);
