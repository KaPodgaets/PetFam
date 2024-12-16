using Microsoft.EntityFrameworkCore;
using PetFam.Discussions.Domain;

namespace PetFam.Discussions.Application.Database;

public interface IDiscussionsReadDbContext
{
    DbSet<Discussion> Discussions { get; }
}