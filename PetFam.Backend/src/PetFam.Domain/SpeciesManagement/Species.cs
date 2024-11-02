﻿using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Domain.SpeciesManagement
{
    public class Species : Entity<SpeciesId>, ISoftDeletable
    {
        private bool _isDeleted = false;
        private List<Breed> _breeds = [];
        // EF Core ctor
        private Species(SpeciesId id) : base(id) { }

        private Species(SpeciesId id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; } = null!;
        public IReadOnlyList<Breed> Breeds => _breeds;

        public static Result<Species> Create(SpeciesId id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValueIsInvalid("name").ToErrorList();

            if (id.Value == Guid.Empty)
                return Errors.General.ValueIsInvalid(nameof(VolunteerId)).ToErrorList();

            return new Species(id, name);
        }

        public Result<Guid> AddBreed(Breed breed)
        {
            var alreadyExist = _breeds.Any(x => x.Name == breed.Name);
            if(alreadyExist)
            {
                return Errors.General.ValueIsInvalid(breed.Name).ToErrorList();
            }

            _breeds.Add(breed);
            return breed.Id.Value;
        }

        public void Delete()
        {
            _isDeleted = true;
        }

        public void Restore()
        {
            _isDeleted = false;
        }
    }
}
