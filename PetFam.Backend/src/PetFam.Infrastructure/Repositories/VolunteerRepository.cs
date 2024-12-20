﻿using Microsoft.EntityFrameworkCore;
using PetFam.Application.VolunteerManagement;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;
using PetFam.Infrastructure.DbContexts;

namespace PetFam.Infrastructure.Repositories
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly WriteDbContext _dbContext;
        public VolunteerRepository(WriteDbContext dbContext)
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
                return Errors.General.Failure().ToErrorList();
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
                return Errors.General.NotFound(id.Value).ToErrorList();
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
                return Errors.General.NotFound(email.Value).ToErrorList();
            }

            return model;
        }

        public async Task<Result<Guid>> Update(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            _dbContext.Volunteers.Attach(volunteer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return volunteer.Id.Value;
        }

        public async Task<Result<Guid>> Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            _dbContext.Volunteers.Remove(volunteer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return volunteer.Id.Value;
        }

        public async Task<Result<IReadOnlyList<Volunteer>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var models = await _dbContext.Volunteers
                .Include(m => m.Pets)
                .ToListAsync(cancellationToken);

            return models;
        }
    }
}
