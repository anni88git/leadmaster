using LeadManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadManager.Data;

public class LeadDbContext : DbContext
{
    public LeadDbContext(DbContextOptions<LeadDbContext> options) : base(options)
    {
    }

    public DbSet<Lead> Leads => Set<Lead>();
}
