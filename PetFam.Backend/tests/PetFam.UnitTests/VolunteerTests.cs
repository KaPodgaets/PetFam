using FluentAssertions;
using PetFam.Domain.SpeciesManagement;
using PetFam.Domain.Volunteer;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.UnitTests
{
    public class VolunteerHelper
    {
        public static Volunteer CreateDummyVolunteer()
        {
            return Volunteer.Create(
                VolunteerId.NewId(),
                FullName.Create("Kosta", "Jhonson", null).Value,
                Email.Create("string@email.com").Value,
                null,
                null).Value;
        }

        public static Pet CreateDummyPet()
        {
            return Pet.Create(
                PetId.NewPetId(),
                "dog",
                SpeciesBreed.Create(
                    SpeciesId.NewId(),
                    Guid.NewGuid()).Value,
                PetStatus.LookingForHome,
                PetGeneralInfo.Create("string", "color", 1, 1, "978265").Value,
                PetHealthInfo.Create("comment", false, DateTime.UtcNow, true).Value,
                Address.Create("country", "city", "street", null, null).Value,
                AccountInfo.Create("number", "bankName").Value,
                DateTime.UtcNow,
                1).Value;
        }
    }
    public class VolunteerTests
    {
        [Fact]
        public void AddPet_ToEmptyPetList_Assign_FirstOrderToPet()
        {
            // Arrange
            var volunteer = VolunteerHelper.CreateDummyVolunteer();

            var pet = VolunteerHelper.CreateDummyPet();

            // Act
            volunteer.AddPet(pet);

            // Assert

            pet.Order.Should().Be(0);
            Assert.Equal(0, pet.Order);
            Assert.Single(volunteer.Pets);
        }

        [Fact]
        public void ChangePetOrder_DoesNotCreateDuplicates()
        {
            // Arrange
            const int petPositionToMove = 3;
            const int newPosition = 5;
            const int petsAmount = 10;

            var volunteer = VolunteerHelper.CreateDummyVolunteer();

            for (int i = 0; i < petsAmount; i++)
            {
                volunteer.AddPet(VolunteerHelper.CreateDummyPet());
            }

            var petToMoveId = volunteer.Pets[petPositionToMove].Id;
            var petToCheckPositionId = volunteer.Pets[newPosition].Id;

            // Act
            volunteer.ChangePetOrder(volunteer.Pets[petPositionToMove], newPosition);

            // Assert
            var petToMove = volunteer.Pets.FirstOrDefault(p => p.Id == petToMoveId);
            var petToCheck = volunteer.Pets.FirstOrDefault(p => p.Id == petToCheckPositionId);

            petToMove?.Order.Should().Be(newPosition);
            petToCheck?.Order.Should().Be(newPosition-1);

            volunteer.Pets.Should().HaveCount(petsAmount);

            var orders = volunteer.Pets.Select(p => p.Order).ToList();

            orders.Should().OnlyHaveUniqueItems();
        }

        
    }
}