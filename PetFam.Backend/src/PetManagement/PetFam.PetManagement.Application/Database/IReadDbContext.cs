using Microsoft.EntityFrameworkCore;
using PetFam.Shared.Dtos;

namespace PetFam.PetManagement.Application.Database
{
    public interface IReadDbContext
    {
        public DbSet<VolunteerDto> Volunteers { get; }
        public DbSet<PetDto> Pets { get; }
    }
}
