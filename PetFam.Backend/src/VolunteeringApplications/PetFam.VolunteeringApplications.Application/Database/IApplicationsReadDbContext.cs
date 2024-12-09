using Microsoft.EntityFrameworkCore;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Application.Database;

public interface IApplicationsReadDbContext
{
    public DbSet<VolunteeringApplication> Applications { get; set; }
}