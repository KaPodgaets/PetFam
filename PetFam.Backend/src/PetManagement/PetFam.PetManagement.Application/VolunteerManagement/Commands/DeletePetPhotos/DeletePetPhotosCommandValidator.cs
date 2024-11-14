namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.DeletePetPhotos;

public class DeletePetPhotosCommandValidator:AbstractValidator<DeletePetPhotosCommand>
{
    public DeletePetPhotosCommandValidator()
    {
        RuleFor(x => x.Paths)
            .ForEach(path => path.NotEmpty());
        
        RuleForEach(x => x.Paths)
            .NotEmpty()
            .Must(ValidPhotoPath)
            .WithMessage("Invalid filename");
        
        RuleFor(x => x.PetId).NotEmpty();
        RuleFor(x => x.VolunteerId).NotEmpty();
    }
    
    private static bool ValidPhotoPath(string filePath)
    {
        var result = PetPhoto.Create(filePath); // Assuming 'isMain' is false or some default
        return result.IsSuccess;
    }
}