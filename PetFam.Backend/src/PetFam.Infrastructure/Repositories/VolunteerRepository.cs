﻿using Microsoft.EntityFrameworkCore;
using PetFam.Application.Volunteers;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Infrastructure.Repositories
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public VolunteerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Add(Volunteer model, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Volunteers.AddAsync(model, cancellationToken);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Errors.General.Failure();
            }

            return model.Id.Value;
        }

        public async Task<Result<Volunteer>> GetById(VolunteerId id, CancellationToken cancellationToken = default)
        {
            var model = await _dbContext.Volunteers
                .Include(m => m.Pets)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);


            if (model == null)
            {
                return Errors.General.NotFound(id.Value);
            }

            return model;
        }

        public async Task<Result<Volunteer>> GetByEmail(Email email, CancellationToken cancellationToken = default)
        {
            var model = await _dbContext.Volunteers
                .Include(m => m.Pets)
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);


            if (model == null)
            {
                return Errors.General.NotFound(email.Value);
            }

            return model;
        }

        public async Task<Result<Guid>> Update(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            _dbContext.Volunteers.Attach(volunteer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return volunteer.Id.Value;
        }
    }
}
